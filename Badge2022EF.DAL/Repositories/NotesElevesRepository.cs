using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
    {
    public class NotesElevesRepository : BaseRepository<NotesEleves>, INoteselevesRepository
    {
        public NotesElevesRepository(Badge2022Context context) : base(context)
        {
        }

        public override NotesEleves GetOne(int id)
        {
            return _db.NotesEleves.Find(id)!.ToModel();
        }
        public override IEnumerable<NotesEleves> GetOne2(int id)
            {
            yield return _db.NotesEleves.Find(id)!.ToModel();
            }
        public override IEnumerable<NotesEleves> GetAll()
            {
            return _db.NotesEleves.Select(m => m.ToModel());
            }
        public IEnumerable<NotesEleves> GetAll(int limit, int offset)
        {
            return _db.NotesEleves.Skip(offset).Take(limit).Select(m => m.ToModel());
        }
        public override bool Add(NotesEleves NotesEleve)
            {
            NotesEleveEntity toInsert = NotesEleve.ToEntity();
            _db.NotesEleves.Add(toInsert);

            try
                {
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                _db.NotesEleves.Remove(toInsert);
                return false;
                }
            }

        public override bool Update(NotesEleves NotesEleve)
        {
            NotesEleveEntity toUpdate = NotesEleve.ToEntity();
            _db.NotesEleves.Update(toUpdate);
            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public override bool Delete(int id) { return true; }
        public bool Delete2(NotesEleves NotesEleve)
        {
            try
                {
                _db.NotesEleves.Remove(NotesEleve.ToEntity());
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                return false;

                }
            }
        }
    }
