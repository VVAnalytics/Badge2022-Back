using Badge2022EF.DAL;
using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.Models.Concretes;
using Badge2022EF.Models.Interfaces;
using Badge2022EF.WebApi.Filters;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
            return new ObservableCollection<Formations>(_FormationRepository.GetAll(limit, offset)).ToList();
        }

        // GET api/<FormationsController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Formations> GetOne(int id)
        {
            IEnumerable<Formations> aa = _FormationRepository.GetOne2(id);
            foreach (var item in aa) { };
            return aa.AsEnumerable().Where(x => x.fid == id);
        }

        // POST api/<FormationsController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Post([FromBody] J_Formations newFormation)
        {
            Formations Formation = new(newFormation.fid, newFormation.fnom);
            _FormationRepository.Add(Formation);
            return Ok();
        }

        // PUT api/<FormationsController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Put(int id, [FromBody] J_Formations majFormation)
        {
            Formations ar = _FormationRepository.GetOne(id);
            if (ar.fid == id)
            {
                Formations Formation = new(id, majFormation.fnom);
                _FormationRepository.Update(Formation);
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/<FormationsController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Delete(int id)
        {
            Formations ar = _FormationRepository.GetOne(id);
            if (ar.fid == id)
            {
                _FormationRepository.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
