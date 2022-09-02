using Badge2022EF.DAL.Repositories;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.Models.Concretes;
using Badge2022EF.Models.Interfaces;
using Badge2022EF.WebApi.Filters;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;


namespace Badge2022EF.WebApi.Controllers
{
    [Authorization]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ExamensController : ControllerBase
    {
        private readonly ExamenRepository _ExamenRepository;

        public ExamensController(ExamenRepository ExamenRepository)
        {
            _ExamenRepository = ExamenRepository;
        }

        // GET: api/<ExamensController>
        [HttpGet]
        [Authorization("Admin", "Praticien")]
        public IEnumerable<Examens> GetAll()
        {
            return new ObservableCollection<Examens>(_ExamenRepository.GetAll()).ToList();
        }
        // GET: api/<MedecinsController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Examens> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            return new ObservableCollection<Examens>(_ExamenRepository.GetAll(limit, offset)).ToList();
        }
        // GET api/<ExamensController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Examens> GetOne(int id)
        {
            IEnumerable<Examens> aa = _ExamenRepository.GetOne2(id);
            foreach (var item in aa) { };
            return aa.AsEnumerable().Where(x => x.eid == id);
        }

        // POST api/<ExamensController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Post([FromBody] J_Examens newExamen)
        {
            Examens Examen = new(newExamen.eid, newExamen.enom, newExamen.enote);
            _ExamenRepository.Add(Examen);
            return Ok();
        }

        // PUT api/<ExamensController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Put(int id, [FromBody] J_Examens majExamen)
        {
            Examens ar = _ExamenRepository.GetOne(id);
            if (ar.eid == id)
            {
                Examens Examen = new(majExamen.eid, majExamen.enom, majExamen.enote);
                _ExamenRepository.Update(Examen);
                return Ok();
            }
            return BadRequest();

        }

        // DELETE api/<ExamensController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Delete(int id)
        {
            Examens ar = _ExamenRepository.GetOne(id);
            if (ar.eid == id)
            {
                _ExamenRepository.Delete(id);
                return Ok();
            }
            return BadRequest();
        }
    }
}
