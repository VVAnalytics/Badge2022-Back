using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
{
    public class MedecinRepository : BaseRepository<NotesEleves>, IFormationsRepository
    {
        public MedecinRepository(Badge2022Context context) : base(context)
        {
        }

        public override NotesEleves GetOne(long id)
        {
            return _db.Medecins.Find(id)!.ToModel();
        }
        public override IEnumerable<NotesEleves> GetOne2(long id)
        {
            yield return _db.Medecins.Find(id)!.ToModel();
        }
        public IEnumerable<NotesEleves> GetAll(int limit, int offset)
        {
            return _db.Medecins.Skip(offset).Take(limit).Select(m => m.ToModel());
        }

        public override IEnumerable<NotesEleves> GetAll()
        {
            return _db.Medecins.Select(m => m.ToModel());
        }

        public override bool Add(NotesEleves Medecin)
        {
            MedecinEntity toInsert = Medecin.ToEntity();
            _db.Medecins.Add(toInsert);

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                _db.Medecins.Remove(toInsert);
                return false;
            }
        }

        public override bool Update(NotesEleves Medecin)
        {
            MedecinEntity toUpdate = _db.Medecins.Find(Medecin.MedecinId)!;
            toUpdate.IdMedecin = Medecin.MedecinId;
            _db.Medecins.Remove(_db.Medecins.Find(Medecin.MedecinId)!);
            toUpdate = Medecin.ToEntity();
            _db.Medecins.Add(toUpdate);

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

        public override bool Delete(long id)
        {
            try
            {
                _db.Medecins.Remove(_db.Medecins.Find(id)!);
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
