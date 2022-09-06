using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.JWT_Authentication.JWTWebAuthentication.Repository;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL;
using Badge2022EF.WebApi.Filters;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Badge2022EF.DAL.Repositories.Mappers;

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
        private readonly Badge2022Context _context;

        public PersonnesController(IJWTManagerRepository jWTManager,
                                    PersonneRepository personneRepository,
                                    SignInManager<PersonneEntity> signInManager,
                                    UserManager<PersonneEntity> userManager,
                                    Badge2022Context context
            )
        {
            _jWTManager = jWTManager;
            _personneRepository = personneRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        [Authorization("Admin")]
        public IEnumerable<PersonneEntity> GetAll()
        {
            return _context.Users.ToList();
        }

        // GET api/<PersonnesController>/5
        [HttpGet]
        [Authorization("Admin", "Etudiant")]
        public Personnes? GetOne([FromQuery] int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id).ToModel();
            //IEnumerable<Personnes> aa = _personneRepository.GetOne2(id);
            //if (aa != null)
            //{
            //    foreach (var item in aa) { _ = item; };
            //    return aa.AsEnumerable();
            //}
            //return null;
        }

        // POST api/<PersonnesController>
        [HttpPost]
        [Authorization("Admin")]
        public async Task<IActionResult> Post([FromBody] PersonneEntity newPersonne)
        {
            PersonneEntity user = new();
            user = _context.Users.FirstOrDefault(x => x.UserName == "admin@admin.be")!;
            user.unom = newPersonne.unom;
            user.uprenom = newPersonne.uprenom;
            user.Email = newPersonne.Email;
            user.urue = newPersonne.urue;
            user.ucodep = newPersonne.ucodep;
            user.uville = newPersonne.uville;
            user.upays = newPersonne.upays == null ? "Belgique" : newPersonne.upays;
            user.UserName = newPersonne.Email;
            user.NormalizedUserName = user.UserName.ToUpper();
            user.PasswordHash = "AQAAAAEAACcQAAAAELC46PwvwvqahSDF3HaR5YffCHhqhUmSICm45Aavt48H8zDk6Ems9QCKde7ZEHJL6g==";
            user.SecurityStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
            user.Id = 0;
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
        [Authorization("Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] PersonneEntity majPersonne)
        {
            PersonneEntity user = new();
            user = _context.Users.FirstOrDefault(x=>x.Id == id)!;
            if (user == null) user = _context.Users.FirstOrDefault(x => x.unom == majPersonne.unom)!;
            if (user != null)
            {
                user.unom = majPersonne.unom;
                user.uprenom = majPersonne.uprenom;
                user.Email = majPersonne.Email;
                user.urue = majPersonne.urue;
                user.ucodep = majPersonne.ucodep;
                user.uville = majPersonne.uville;
                user.upays = majPersonne.upays;
                var result = await _signInManager.CheckPasswordSignInAsync(user, user.PasswordHash, false);
                if (result.Succeeded)
                {
                    user.SecurityStamp = (DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds.ToString();
                }
                try
                {
                    _context.Update(user);
                    _context.SaveChanges();
                    return Ok();
                }
                catch (Exception)
                {
                    _context.Personnes.Remove(user);
                    return BadRequest();
                }
            }
            return Ok();
        }

        // DELETE api/<PersonnesController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            PersonneEntity user = await _userManager.FindByIdAsync(id.ToString());
            try
            {
                _context.Personnes.Remove(user);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
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

                    var p4 = await _userManager.GetRolesAsync(p);

                    BigUsers.Role = p4[0];

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
    }
}
