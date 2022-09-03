using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
    {
    public class CoursRepository : BaseRepository<Cours>, ICoursRepository
        {
        private readonly Badge2022Context _context;
        public CoursRepository(Badge2022Context context) : base(context)
        {
            _context = context;
        }

        public override Cours GetOne(int id)
            {
            return _db.Cours.Find(id)!.ToModel();
            }
        public override IEnumerable<Cours> GetOne2(int id)
        {
            yield return _db.Cours.Find(id)!.ToModel();
        }
        public override IEnumerable<Cours> GetAll()
            {
            return _db.Cours.Select(m => m.ToModel());
            }
        public IEnumerable<Cours> GetAll(int limit, int offset)
        {
            return _db.Cours.Skip(offset).Take(limit).Select(m => m.ToModel());
        }

        public override bool Add(Cours cours)
            {
            CoursEntity toInsert = cours.ToEntity();
            _db.Cours.Add(toInsert);

            try
                {
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                _db.Cours.Remove(toInsert);
                return false;
                }
            }

        public override bool Update(Cours cours)
        {
            CoursEntity toUpdate = _db.Cours.Find(cours.cid)!;
            toUpdate.cid = cours.cid;
            _db.Cours.Remove(_db.Cours.Find(cours.cid)!);
            toUpdate = cours.ToEntity();
            _db.Cours.Add(toUpdate);

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
                _db.Cours.Remove(_db.Cours.Find(id)!);
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
