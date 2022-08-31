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
    public class FormationsController : ControllerBase
    {
        private readonly FormationsRepository _FormationRepository;
        private readonly Badge2022Context _context;
        private string? _jWTEmail = string.Empty;

        public FormationsController(FormationsRepository FormationRepository, Badge2022Context context)
        {
            _FormationRepository = FormationRepository;
            _context = context;
        }

        // GET: api/<FormationsController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Formations> GetAll()
        {
            return new ObservableCollection<Formations>(_FormationRepository.GetAll()).ToList();
        }
        // GET: api/<MedecinsController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Formations> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            return new ObservableCollection<Formations>(_FormationRepository.GetAll(limit, offset)).ToList().Where(x => x.fid == p.Id);
        }

        // GET api/<FormationsController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Formations> GetOne(int id)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            IEnumerable<Formations> aa = _FormationRepository.GetOne2(id);
            foreach (var item in aa) { };
            return aa.AsEnumerable().Where(x => x.fid == p.Id);
        }

        // POST api/<FormationsController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post([FromBody] J_Formations newFormation)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            Formations Formation = new(
                        newFormation.fid,
                        newFormation.fnom
                        )
            {
                fid = p.Id
            };
            _FormationRepository.Add(Formation);
        }

        // PUT api/<FormationsController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put(long id, [FromBody] J_Formations majFormation)
        {
            PersonneEntity p = new();
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                _jWTEmail = identity?.FindFirst(ClaimTypes.Name)?.Value;
                p = (PersonneEntity)_context.Users.Include(x => x.urole).ToList().Where(x => x.Email == _jWTEmail);
            };
            Formations Formation = new(
                        majFormation.fid,
                        majFormation.fnom
                        )
            {
            };
            _FormationRepository.Add(Formation);
        }

        // DELETE api/<FormationsController>/5
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
            Formations ar = _FormationRepository.GetOne(id);
            if (ar.fid == p.Id) _FormationRepository.Delete(id);
            _FormationRepository.Delete(id);
        }
    }
}
