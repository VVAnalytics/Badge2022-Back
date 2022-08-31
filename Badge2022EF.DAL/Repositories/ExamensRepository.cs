using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;

using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
    {
    public class ExamenRepository : BaseRepository<Examens>, IExamensRepository
        {
        public ExamenRepository(Badge2022Context context) : base(context)
        {
        }

        public override Examens GetOne(int id)
        {
            return _db.Examens.Find(id)!.ToModel();
        }
        public override IEnumerable<Examens> GetOne2(int id)
            {
            yield return _db.Examens.Find(id)!.ToModel();
            }
        public IEnumerable<Examens> GetAll(int limit, int offset)
        {
            return _db.Examens.Skip(offset).Take(limit).Select(m => m.ToModel());
        }
        public override IEnumerable<Examens> GetAll()
            {
            return _db.Examens.Select(m => m.ToModel());
            }

        public override bool Add(Examens Examen)
            {
            ExamenEntity toInsert = Examen.ToEntity();
            _db.Examens.Add(toInsert);

            try
                {
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                _db.Examens.Remove(toInsert);
                return false;
                }
            }

        public override bool Update(Examens Examen)
        {
            ExamenEntity toUpdate = _db.Examens.Find(Examen.eid)!;
            toUpdate.eid = Examen.eid;
            _db.Examens.Remove(_db.Examens.Find(Examen.eid)!);
            toUpdate = Examen.ToEntity();
            _db.Examens.Add(toUpdate);

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

        public override bool Delete(int id)
            {
            try
                {
                _db.Examens.Remove(_db.Examens.Find(id)!);
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
