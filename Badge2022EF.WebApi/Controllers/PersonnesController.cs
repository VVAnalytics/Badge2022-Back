﻿using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.JWT_Authentication.JWTWebAuthentication.Repository;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;
using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL;
using Badge2022EF.WebApi.Filters;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.WebApi.Controllers
{
    [Authorization]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PersonnesController : ControllerBase
    {
        private readonly IJWTManagerRepository _jWTManager;
        private readonly PersonneRepository _personneRepository;
        private readonly SignInManager<PersonneEntity> _signInManager;
        private readonly UserManager<PersonneEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;
        private readonly Badge2022Context _context;

        public PersonnesController( IJWTManagerRepository jWTManager, 
                                    PersonneRepository personneRepository, 
                                    SignInManager<PersonneEntity> signInManager, 
                                    UserManager<PersonneEntity> userManager, 
                                    RoleManager<RoleEntity> roleManager,
                                    Badge2022Context context
            )
        {
            _jWTManager = jWTManager;
            _personneRepository = personneRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: api/<PersonnesController>
        [HttpGet]
        [Authorization("Admin", "Praticien")]
        public IEnumerable<PersonneEntity> GetAll()
        {
            return _context.Users.Include(x => x.urole).ToList() ;
        }

        [HttpGet]
        [Authorization("Admin", "Praticien")]
        public IEnumerable<PersonneEntity> GetAllAs([FromQuery] string connectAs)
        {
            return new ObservableCollection<PersonneEntity>(_context.Users.Include(x => x.urole).ToList());
        }

        // GET api/<PersonnesController>/5
        [HttpGet]
        [Authorization("Admin", "Praticien")]
        public IEnumerable<Personnes>? GetOne([FromQuery] int id)
        {
            IEnumerable<Personnes> aa = _personneRepository.GetOne2(id);
            if (aa != null) {
                foreach (var item in aa) { _ = item; };
                return aa.AsEnumerable();
            }
            return null;
        }

        // POST api/<PersonnesController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Post([FromQuery] J_Personnes newPersonne)
        {
            PersonneEntity user = new();
            var result = await _signInManager.CheckPasswordSignInAsync(user, newPersonne.PasswordHash, false);
            if (result.Succeeded)
            {
                user.Email = newPersonne.Email;
                user.UserName = newPersonne.Email;
                user.EmailConfirmed = true;
                user.SecurityStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            }
            try
            {
                _context.Personnes.Add(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                _context.Personnes.Remove(user);
                return BadRequest();
            }
        }
        // PUT api/<PersonnesController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Put(int id, [FromBody] J_Personnes majPersonne)
        {
            PersonneEntity user = await _userManager.FindByIdAsync(id.ToString());
            var result = await _signInManager.CheckPasswordSignInAsync(user, majPersonne.PasswordHash, false);
            if (result.Succeeded)
            {
                user.Email = majPersonne.Email;
                user.UserName = majPersonne.Email;
                user.EmailConfirmed = true;
                user.SecurityStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            }
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                _context.Personnes.Remove(user);
                return BadRequest();
            }
        }

        // DELETE api/<PersonnesController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Delete(int id)
        {
            PersonneEntity user = await _userManager.FindByIdAsync(id.ToString());
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                _context.Personnes.Remove(user);
                return BadRequest();
            }
        }

        // Login ouvert à tout le monde
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            PersonneEntity p = await _userManager.FindByNameAsync(email);
            if (p != null)
            {
                    var result = await _signInManager.CheckPasswordSignInAsync(p, password, false);
                    if (result.Succeeded)
                    {
                        J_Users BigUsers = new()
                        {
                            Email = email
                        };
                        RoleEntity q = await _roleManager.FindByIdAsync(p.urole.Select(x=> x.Id).ToString());
                        BigUsers.Role = q.Name;

                        var token = _jWTManager.Authenticate(BigUsers);
                        if (token == null)
                        {
                            return Unauthorized();
                        }
                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
            }
            else
            {
                return Unauthorized();
            }
        }

        // tous les patients ont connectAs = email par defaut => n'ont pas praticien : Ouvert a tout le monde
        [HttpPost]
        public async Task<IActionResult> RegisterPatient(string email, string password)
        {
            PersonneEntity p = await _userManager.FindByNameAsync(email);
            if (p == null)
            {
                PersonneEntity user = new()
                {
                    Email = email,
                    UserName = email,
                    EmailConfirmed = true,
                    SecurityStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString()
                };
                var result = await _userManager.CreateAsync(user, password);
                try
                {
                    _context.SaveChanges();
                    PersonneEntity r = await _userManager.FindByNameAsync(email);
                    return Ok();
                }
                catch (Exception)
                {
                    _context.Personnes.Remove(user);
                    return BadRequest(result.Errors);
                }
            }
            else {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorization("Admin")]
        // C'est l'admin qui peut enregistrer un praticien
        public async Task<IActionResult> RegisterPraticien(string emailPraticien, string passwordPraticien)
        {
            PersonneEntity p = await _userManager.FindByNameAsync(emailPraticien);
            if (p == null)
            {
                    PersonneEntity user = new()
                    {
                        Email = emailPraticien,
                        UserName = emailPraticien,
                        EmailConfirmed = true,
                        SecurityStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString()
                    };
                _ = await _userManager.CreateAsync(user, passwordPraticien);
                try
                    {
                        _context.SaveChanges();
                        return Ok();
                    }
                    catch (Exception)
                    {
                        _context.Personnes.Remove(user);
                        return BadRequest();
                    }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorization("Admin", "Praticien")]
        // le patient prend dans connectas l'émail du praticien => vérification ligin du praticien
        public async Task<IActionResult> RegisterPatientToPraticien(string praticienEmail, string praticienPassword, string patientEmail)
        {
            PersonneEntity p = await _userManager.FindByNameAsync(patientEmail);
            if (p != null)
            {
                PersonneEntity q = await _userManager.FindByNameAsync(praticienEmail);
                if (q != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(p, praticienPassword, false);
                    if (result.Succeeded)
                    {
                        try
                        {
                            _context.SaveChanges();
                            return Ok();
                        }
                        catch (Exception)
                        {
                            _context.Personnes.Remove(p);
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }        
    }
}
