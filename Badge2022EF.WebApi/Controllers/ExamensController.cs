using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Concretes;
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
        public Examens GetOne(int id)
        {
            return _ExamenRepository.GetOne(id);
        }

        // POST api/<ExamensController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post([FromBody] J_Examens newExamen)
        {
            Examens Examen = new(
                        newExamen.ExamenUrl,
                        newExamen.ExamenNom,
                        newExamen.ExamenTitulaires,
                        newExamen.ExamenRue,
                        newExamen.ExamenVilles,
                        newExamen.ExamenDepartement,
                        newExamen.ExamenRegion
                        )
            {
                ExamenId = 0
            };
            _ExamenRepository.Add(Examen);
        }

        // PUT api/<ExamensController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put(long id, [FromBody] J_Examens majExamen)
        {
            Examens Examen = new(
                        majExamen.ExamenUrl,
                        majExamen.ExamenNom,
                        majExamen.ExamenTitulaires,
                        majExamen.ExamenRue,
                        majExamen.ExamenVilles,
                        majExamen.ExamenDepartement,
                        majExamen.ExamenRegion
                        )
            {
                ExamenId = id
            };
            _ExamenRepository.Update(Examen);
        }

        // DELETE api/<ExamensController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Delete(int id)
        {
            _ExamenRepository.Delete(id);
        }
    }
}
