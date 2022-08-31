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
    public class MedecinsController : ControllerBase
    {
        private readonly MedecinRepository _medecinRepository;
        public MedecinsController(MedecinRepository medecinRepository)
        {
            _medecinRepository = medecinRepository;
        }

        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetAll()
        {
            return new ObservableCollection<NotesEleves>(_medecinRepository.GetAll()).ToList();
        }

        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            return new ObservableCollection<NotesEleves>(_medecinRepository.GetAll(limit, offset)).ToList();
        }

        // GET api/<MedecinsController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetOne(long id)
        {
            IEnumerable<NotesEleves> aa = _medecinRepository.GetOne2(id);
            foreach (var item in aa)  { _ = item; };
            return aa.AsEnumerable();
        }
        // POST api/<MedecinsController>/create
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post(J_Medecins newMedecin)
        //public void Post(J_Medecins newMedecin)
        {
            NotesEleves medecin = new(
                                    newMedecin.MedecinName,
                                    newMedecin.MedecinInami,
                                    newMedecin.MedecinRue,
                                    newMedecin.MedecinVille,
                                    newMedecin.MedecinTelephone,
                                    newMedecin.MedecinGsm,
                                    newMedecin.MedecinFax,
                                    newMedecin.MedecinEmail
                                    )
            {
                MedecinId = 0
            };
            _medecinRepository.Add(medecin);
        }

        // PUT api/<MedecinsController>/update/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put([FromQuery] long id, [FromQuery] J_Medecins majMedecin)
        {
            NotesEleves medecin = new(
                        majMedecin.MedecinName,
                        majMedecin.MedecinInami,
                        majMedecin.MedecinRue,
                        majMedecin.MedecinVille,
                        majMedecin.MedecinTelephone,
                        majMedecin.MedecinGsm,
                        majMedecin.MedecinFax,
                        majMedecin.MedecinEmail
                        )
            {
                MedecinId = id
            };
            _medecinRepository.Update(medecin);
        }

        // DELETE api/<MedecinsController>/delete/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Delete(long id)
        {
            _medecinRepository.Delete(id);
        }
    }
}
