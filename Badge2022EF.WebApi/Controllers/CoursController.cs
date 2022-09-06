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
            return new ObservableCollection<Cours>(_CoursRepository.GetAll(limit, offset)).ToList();
        }

        // GET api/<CoursController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Cours> GetOne(int id)
        {
            IEnumerable<Cours> aa = _CoursRepository.GetOne2(id);
            foreach (var item in aa) { };
            return aa.AsEnumerable().Where(x => x.cid == id);
        }

        // POST api/<CoursController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Post([FromBody] J_Cours newCours)
        {
            Cours cours = new(newCours.cid, newCours.cnom);
            _CoursRepository.Add(cours);
            return Ok();
        }

        // PUT api/<CoursController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Put(int id, [FromBody] J_Cours majCours)
        {
            Cours ar = _CoursRepository.GetOne(id);
            if (ar.cid == id)
            {
                Cours Cours = new(id, majCours.cnom);
                _CoursRepository.Update(Cours);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<CoursController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Delete(int id)
        {
            Cours ar = _CoursRepository.GetOne(id);
            if (ar.cid == id) { 
                _CoursRepository.Delete(id);
                return Ok();
            }
            return BadRequest();
        }

        public int GetDiscountedPrice(int price)
        {
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                return price / 2;
            }
            else
            {
                return price;
            }
        }
    }
}
