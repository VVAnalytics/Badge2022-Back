using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;

using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
    {
    public class ArmoireRepository : BaseRepository<Cours>, IExamensRepository
        {
        public ArmoireRepository(Badge2022Context context) : base(context)
        {
        }

        public override Cours GetOne(long id)
        {
            return _db.Armoires.Find(id)!.ToModel();
        }
        public override IEnumerable<Cours> GetOne2(long id)
            {
            yield return _db.Armoires.Find(id)!.ToModel();
            }
        public IEnumerable<Cours> GetAll(int limit, int offset)
        {
            return _db.Armoires.Skip(offset).Take(limit).Select(m => m.ToModel());
        }
        public override IEnumerable<Cours> GetAll()
            {
            return _db.Armoires.Select(m => m.ToModel());
            }

        public override bool Add(Cours Armoire)
            {
            ArmoireEntity toInsert = Armoire.ToEntity();
            _db.Armoires.Add(toInsert);

            try
                {
                _db.SaveChanges();
                return true;
                }
            catch (DbUpdateException)
                {
                _db.Armoires.Remove(toInsert);
                return false;
                }
            }

        public override bool Update(Cours Armoire)
        {
            ArmoireEntity toUpdate = _db.Armoires.Find(Armoire.ArmoID)!;
            toUpdate.Id = Armoire.ArmoID;
            _db.Armoires.Remove(_db.Armoires.Find(Armoire.ArmoID)!);
            toUpdate = Armoire.ToEntity();
            _db.Armoires.Add(toUpdate);

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
                _db.Armoires.Remove(_db.Armoires.Find(id)!);
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
