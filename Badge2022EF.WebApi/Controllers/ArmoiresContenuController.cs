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
    public class ArmoiresContenuController : ControllerBase
    {
        private readonly ArmoiresContenuRepository _armoiresContenuRepository;

        public ArmoiresContenuController(ArmoiresContenuRepository armoiresContenuRepository)
        {
           _armoiresContenuRepository = armoiresContenuRepository;
        }

        // GET: api/<ArmoiresContenuController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Examens> GetAll()
        {
            return new ObservableCollection<Examens>(_armoiresContenuRepository.GetAll()).ToList();
        }
        // GET: api/<MedecinsController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Examens> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            return new ObservableCollection<Examens>(_armoiresContenuRepository.GetAll(limit, offset)).ToList();
        }
        // GET api/<ArmoiresContenuController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<Examens> GetOne(long id)
        {
            IEnumerable<Examens> aa = _armoiresContenuRepository.GetOne2(id);
            foreach (var item in aa) { _ = item; };
            return aa.AsEnumerable();
        }

        // POST api/<ArmoiresContenuController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post([FromQuery] J_ArmoiresContenu newArmoiresContenu)
        {
            Examens armoiresContenu = new(
                        newArmoiresContenu.ACmedicamentId,
                        newArmoiresContenu.ACarmoireId,
                        newArmoiresContenu.ACordonnanceId,
                        newArmoiresContenu.ACquantite,
                        newArmoiresContenu.ACmediId
                        );
            newArmoiresContenu.ACarmoireId = 0;
            _armoiresContenuRepository.Add(armoiresContenu);
        }

        // PUT api/<ArmoiresContenuController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put(long id, [FromQuery] J_ArmoiresContenu majArmoiresContenu)
        {
            Examens armoiresContenu = new(
                        majArmoiresContenu.ACmedicamentId,
                        majArmoiresContenu.ACarmoireId,
                        majArmoiresContenu.ACordonnanceId,
                        majArmoiresContenu.ACquantite,
                        majArmoiresContenu.ACmediId
                        );
            majArmoiresContenu.ACarmoireId = id;
            _armoiresContenuRepository.Update(armoiresContenu);
        }

        // DELETE api/<ArmoiresContenuController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Delete(long id)
        {
            _armoiresContenuRepository.Delete(id);
        }
    }
}
