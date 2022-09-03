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
        [Authorization("Admin", "Praticien")]
        public IEnumerable<PersonneEntity> GetAll()
        {
            return _context.Users.ToList();
        }

        // GET api/<PersonnesController>/5
        [HttpGet]
        [Authorization("Admin", "Etudiant")]
        public IEnumerable<Personnes>? GetOne([FromQuery] int id)
        {
            IEnumerable<Personnes> aa = _personneRepository.GetOne2(id);
            if (aa != null)
            {
                foreach (var item in aa) { _ = item; };
                return aa.AsEnumerable();
            }
            return null;
        }

        // POST api/<PersonnesController>
        [HttpPost]
        [Authorization("Admin")]
        public async Task<IActionResult> Post([FromQuery] J_Personnes newPersonne)
        {
            PersonneEntity user = new();
            user.unom = newPersonne.unom;
            user.uprenom = newPersonne.uprenom;
            user.Email = newPersonne.Email;
            user.udate = newPersonne.udate;
            user.urue = newPersonne.urue;
            user.ucodep = newPersonne.ucodep;
            user.uville = newPersonne.uville;
            user.upays = newPersonne.upays;
            //user.Id = newPersonne.Id ;
            user.UserName = newPersonne.UserName;
            user.NormalizedUserName = newPersonne.NormalizedUserName;
            user.Email = newPersonne.Email;
            user.NormalizedEmail = newPersonne.NormalizedEmail;
            user.EmailConfirmed = newPersonne.EmailConfirmed;
            user.PasswordHash = newPersonne.PasswordHash;
            user.SecurityStamp = newPersonne.SecurityStamp;
            user.ConcurrencyStamp = newPersonne.ConcurrencyStamp;
            user.PhoneNumber = newPersonne.PhoneNumber;
            user.PhoneNumberConfirmed = newPersonne.PhoneNumberConfirmed;
            user.TwoFactorEnabled = newPersonne.TwoFactorEnabled;
            user.LockoutEnd = newPersonne.LockoutEnd;
            user.LockoutEnabled = newPersonne.LockoutEnabled;
            user.AccessFailedCount = newPersonne.AccessFailedCount;

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
        [Authorization("Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] J_Personnes majPersonne)
        {
            PersonneEntity user = new();
            user = await _userManager.FindByIdAsync(id.ToString());
            user.unom = majPersonne.unom;
            user.uprenom = majPersonne.uprenom;
            user.Email = majPersonne.Email;
            user.udate = majPersonne.udate;
            user.urue = majPersonne.urue;
            user.ucodep = majPersonne.ucodep;
            user.uville = majPersonne.uville;
            user.upays = majPersonne.upays;
            //user.Id = newPersonne.Id ;
            user.UserName = majPersonne.UserName;
            user.NormalizedUserName = majPersonne.NormalizedUserName;
            user.Email = majPersonne.Email;
            user.NormalizedEmail = majPersonne.NormalizedEmail;
            user.EmailConfirmed = majPersonne.EmailConfirmed;
            user.PasswordHash = majPersonne.PasswordHash;
            user.SecurityStamp = majPersonne.SecurityStamp;
            user.ConcurrencyStamp = majPersonne.ConcurrencyStamp;
            user.PhoneNumber = majPersonne.PhoneNumber;
            user.PhoneNumberConfirmed = majPersonne.PhoneNumberConfirmed;
            user.TwoFactorEnabled = majPersonne.TwoFactorEnabled;
            user.LockoutEnd = majPersonne.LockoutEnd;
            user.LockoutEnabled = majPersonne.LockoutEnabled;
            user.AccessFailedCount = majPersonne.AccessFailedCount;

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

                    //await _userManager.AddToRoleAsync(p, "Admin");
                    //var userRole2 = await _roleManager.FindByNameAsync("Admin");
                    //await _userManager.UpdateAsync(p);
                    //await _roleManager.UpdateAsync(userRole2);

                    //RoleEntity q = await _roleManager.FindByIdAsync(p.urole.Select(x=> x.Id).ToString());
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
