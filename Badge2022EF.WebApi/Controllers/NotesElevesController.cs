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
    public class NotesElevesController : ControllerBase
    {
        private readonly NotesElevesRepository _notesEleveRepository;
        public NotesElevesController(NotesElevesRepository NotesEleveRepository)
        {
            _notesEleveRepository = NotesEleveRepository;
        }

        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetAll()
        {
            return new ObservableCollection<NotesEleves>(_notesEleveRepository.GetAll()).ToList();
        }

        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            return new ObservableCollection<NotesEleves>(_notesEleveRepository.GetAll(limit, offset)).ToList();
        }

        // GET api/<NotesElevesController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetOne(int id)
        {
            IEnumerable<NotesEleves> aa = _notesEleveRepository.GetOne2(id);
            foreach (var item in aa)  { _ = item; };
            return aa.AsEnumerable();
        }
        // POST api/<NotesElevesController>/create
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public void Post(J_NotesEleves newNotesEleve)
        //public void Post(J_NotesEleves newNotesEleve)
        {
            NotesEleves NotesEleve = new(
                        newNotesEleve.npid,
                        newNotesEleve.ncid,
                        newNotesEleve.nnote
                                    )
            {
            };
            _notesEleveRepository.Add(NotesEleve);
        }

        // PUT api/<NotesElevesController>/update/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Put([FromQuery] int id, [FromQuery] J_NotesEleves majNotesEleve)
        {
            NotesEleves NotesEleve = new(
                        majNotesEleve.npid,
                        majNotesEleve.ncid,
                        majNotesEleve.nnote
                        )
            {
            };
            _notesEleveRepository.Update(NotesEleve);
        }

        // DELETE api/<NotesElevesController>/delete/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public void Delete(int id)
        {
            _notesEleveRepository.Delete(id);
        }
    }
}
