using Badge2022EF.DAL;
using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.Filters;
using Badge2022EF.WebApi.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;
using System.Security.Claims;

namespace Badge2022EF.WebApi.Controllers
{
    [Authorization]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CoursController : ControllerBase
    {
        private readonly CoursRepository _CoursRepository;
        private readonly Badge2022Context _context;
        private string? _jWTEmail = string.Empty;

        public CoursController(CoursRepository CoursRepository, Badge2022Context context)
        {
            _CoursRepository = CoursRepository;
            _context = context;
        }

        // GET: api/<CoursController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Cours> GetAll()
        {
            return new ObservableCollection<Cours>(_CoursRepository.GetAll()).ToList();
        }
        // GET: api/<MedecinsController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Cours> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            return new ObservableCollection<Cours>(_CoursRepository.GetAll(limit, offset)).ToList().Where(x => x.cid == p.Id);
        }

        // GET api/<CoursController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Cours> GetOne(int id)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            IEnumerable<Cours> aa = _CoursRepository.GetOne2(id);
            foreach (var item in aa) { };
            return aa.AsEnumerable().Where(x => x.cid == p.Id);
        }

        // POST api/<CoursController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post([FromBody] J_Cours newCours)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            Cours Cours = new(
                        newCours.cid,
                        newCours.cnom
                        )
            {
                cid = p.Id
            };
            _CoursRepository.Add(Cours);
        }

        // PUT api/<CoursController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put(long id, [FromBody] J_Cours majCours)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            Cours Cours = new(
                        majCours.cid,
                        majCours.cnom
                        )
            {
            };
            _CoursRepository.Add(Cours);
        }

        // DELETE api/<CoursController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Delete(int id)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            Cours ar = _CoursRepository.GetOne(id);
            if (ar.cid == p.Id) _CoursRepository.Delete(id);
            _CoursRepository.Delete(id);
        }
    }
}
