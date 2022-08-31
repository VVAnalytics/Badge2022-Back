using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.Filters;
using Badge2022EF.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.ObjectModel;
using System.Security.Claims;

namespace Badge2022EF.WebApi.Controllers
{
    [Authorization]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class NotesElevesController : ControllerBase
    {
        private readonly NotesElevesRepository _NotesEleveRepository;
        private readonly PersonneRepository _personneRepository;
        private readonly UserManager<PersonneEntity> _userManager;

        public NotesElevesController(PersonneRepository personneRepository, NotesElevesRepository NotesEleveRepository , UserManager<PersonneEntity> userManager)
        {
            _personneRepository = personneRepository;
            _NotesEleveRepository = NotesEleveRepository;
            _userManager = userManager;
        }

        // GET: api/<NotesElevesController>
        [HttpGet]
        [Authorization("Admin", "Praticien", "Patient")]
        public async Task<IEnumerable<Cours>?> GetAll()
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                PersonneEntity user = await _userManager.FindByNameAsync(identity?.FindFirst(ClaimTypes.Name)?.Value);
                ObservableCollection<Cours> bb = new();
                var cc = _NotesEleveRepository.GetAll().ToList();
                if (identity?.FindFirst(ClaimTypes.Role)?.Value == "Admin")
                {
                    return new ObservableCollection<Cours>(cc);
                }
                else if (identity?.FindFirst(ClaimTypes.Role)?.Value == "Praticien")
                {
                    foreach (Personnes item in _personneRepository.GetAll())
                    {
                        foreach (Cours item2 in cc.Where(x => x.ncid == item.Id)) { bb.Add(item2); }
                    }
                    return new ObservableCollection<Cours>(bb);
                }
                else if (identity?.FindFirst(ClaimTypes.Role)?.Value == "Patient")
                {
                    foreach (Personnes item in _personneRepository.GetAll()
                                                                  .Where(x => x.Email == user.Email)
                                                                  .Where(z => z.Isactive = true))
                    {
                        foreach (Cours item2 in cc.Where(x => x.ArmoPatient == item.Id)) { bb.Add(item2); }
                    }
                    return new ObservableCollection<Cours>(bb);
                }
            }
            return null;
        }
        [HttpGet]
        [Authorization("Admin")]
        public IEnumerable<Cours>? GetPage([FromQuery] int limit = 20, [FromQuery] int offset = 0)
        {
            if (HttpContext.User.Identity is ClaimsIdentity identity)
            {
                var cc = _NotesEleveRepository.GetAll(limit, offset).ToList();
                if (identity?.FindFirst(ClaimTypes.Role)?.Value == "Admin")
                {
                    return new ObservableCollection<Cours>(cc);
                }
            }
            return null;
        }

        // GET api/<NotesElevesController>/5
        [HttpGet("{id}")]
        [Authorization("Admin", "Praticien", "Patient")]
        public async Task<IEnumerable<Cours>?> GetOne(long id)
        {
            return (await GetAll())?.AsEnumerable().Where(x => x.ArmoID == id);
        }

        // POST api/<NotesElevesController>
        [HttpPost]
        [Authorization("Admin", "Praticien")]
        public async Task<bool>Post([FromBody] J_NotesEleves newNotesEleve)
        {
            var result = await GetOne(newNotesEleve.ArmoID);
            if (result is not null && result.Count() < 1)
            {
                Cours NotesEleve = new(
                            newNotesEleve.ArmoID,
                            newNotesEleve.ArmoName,
                            newNotesEleve.ArmoPatient
                            );
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var user = _userManager.FindByNameAsync(identity?.FindFirst(ClaimTypes.Name)?.Value);
                    NotesEleve.ArmoID = 0;
                    NotesEleve.ArmoPatient = user.Result.Id;
                    _NotesEleveRepository.Add(NotesEleve);
                    return true;
                }
            }
            return false;
        }

        // PUT api/<NotesElevesController>/5
        [HttpPut("{id}")]
        [Authorization("Admin", "Praticien")]
        public async Task<bool>Put(long id, [FromQuery] J_NotesEleves majNotesEleve)
        {
            var result = await GetOne(majNotesEleve.ArmoID);
            if (result is not null && result.Count() > 0)
            {
                Cours NotesEleve = new(
                        majNotesEleve.ArmoID,
                        majNotesEleve.ArmoName,
                        majNotesEleve.ArmoPatient
                        );
                if (HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var user = _userManager.FindByNameAsync(identity?.FindFirst(ClaimTypes.Name)?.Value);
                    NotesEleve.ArmoID = id;
                    NotesEleve.ArmoPatient = user.Result.Id;
                    _NotesEleveRepository.Update(NotesEleve);
                    return true;
                }
            }
            return false;
        }

        // DELETE api/<NotesElevesController>/5
        [HttpDelete("{id}")]
        [Authorization("Admin")]
        public async Task<bool?> Delete(long id)
        {
            var result = await GetOne(id);
            if (result is not null && result.Count() > 0) { _NotesEleveRepository.Delete(id); return true; }
            return false;
        }
    }
}
