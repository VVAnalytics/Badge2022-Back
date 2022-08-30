using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
    {
    public class OrdonnanceRepository : BaseRepository<Formations>, IOrdonnanceRepository
        {
        public OrdonnanceRepository(Badge2022Context context) : base(context)
        {
        }

        public override Formations GetOne(long id)
            {
            return _db.Ordonnances.Find(id)!.ToModel();
            }
        public override IEnumerable<Formations> GetOne2(long id)
        {
            yield return _db.Ordonnances.Find(id)!.ToModel();
        }
        public override IEnumerable<Formations> GetAll()
            {
            return _db.Ordonnances.Select(m => m.ToModel());
            }
        public IEnumerable<Formations> GetAll(int limit, int offset)
        {
            return _db.Ordonnances.Skip(offset).Take(limit).Select(m => m.ToModel());
        }

        public override bool Add(Formations Ordonnance)
            {
            OrdonnanceEntity toInsert = Ordonnance.ToEntity();
            _db.Ordonnances.Add(toInsert);

            try
                {
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                _db.Ordonnances.Remove(toInsert);
                return false;
                }
            }

        public override bool Update(Formations Ordonnance)
        {
            OrdonnanceEntity toUpdate = _db.Ordonnances.Find(Ordonnance.OrdonnanceId)!;
            toUpdate.Id = Ordonnance.OrdonnanceId;
            _db.Ordonnances.Remove(_db.Ordonnances.Find(Ordonnance.OrdonnanceId)!);
            toUpdate = Ordonnance.ToEntity();
            _db.Ordonnances.Add(toUpdate);

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
                _db.Ordonnances.Remove(_db.Ordonnances.Find(id)!);
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
