using Badge2022EF.DAL.Repositories;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.Filters;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

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
        public IEnumerable<NotesEleves> GetOneNote(NotesEleves notesEleves)
        {
            IEnumerable<NotesEleves> aa = _notesEleveRepository.GetAll().Where(x => x.npid == notesEleves.npid && x.ncid == notesEleves.ncid);
            foreach (var item in aa) { _ = item; };
            return aa.AsEnumerable().ToList();
        }

        // GET api/<NotesElevesController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetOneEleve(int id)
        {
            IEnumerable<NotesEleves> aa = _notesEleveRepository.GetOne2(id).Where(x => x.npid == id);
            foreach (var item in aa)  { _ = item; };
            return aa.AsEnumerable();
        }
        // GET api/<NotesElevesController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public IEnumerable<NotesEleves> GetOneCours(int id)
        {
            IEnumerable<NotesEleves> aa = _notesEleveRepository.GetOne2(id).Where(x => x.ncid == id);
            foreach (var item in aa) { _ = item; };
            return aa.AsEnumerable();
        }

        // POST api/<NotesElevesController>/create
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Post([FromBody] J_NotesEleves newNotesEleve)
        {
            NotesEleves NotesEleve = new(newNotesEleve.npid, newNotesEleve.ncid, newNotesEleve.nnote);
            _notesEleveRepository.Add(NotesEleve);
            return Ok();
        }

        // PUT api/<NotesElevesController>/update/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Put([FromQuery] int pid, int cid, [FromQuery] J_NotesEleves majNotesEleve)
        {
            NotesEleves NotesEleve = new(pid, cid, majNotesEleve.nnote);
            NotesEleves ar = this.GetOneNote(NotesEleve).FirstOrDefault();
            if (ar.npid == pid && ar.ncid == cid)
            {
                _notesEleveRepository.Update(NotesEleve);
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/<NotesElevesController>/delete/5
        [HttpDelete("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<IActionResult> Delete(NotesEleves notesEleves)
        {
            NotesEleves ar = this.GetOneNote(notesEleves).FirstOrDefault();
            if (ar.npid == notesEleves.npid && ar.ncid == notesEleves.ncid)
            {
                _notesEleveRepository.Delete2(notesEleves);
                return Ok();
            }
            return BadRequest();



        }
    }
}
