using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
{
    public class FormationsRepository : BaseRepository<Formations>, IFormationsRepository
    {
        public FormationsRepository(
            Badge2022Context context) : base(context)
        {
        }

        public override Formations GetOne(int id)
        {
            return _db.Formations.Find(id)!.ToModel();
        }
        public override IEnumerable<Formations> GetOne2(int id)
        {
            yield return _db.Formations.Find(id)!.ToModel();
        }
        public override IEnumerable<Formations> GetAll()
        {
            return _db.Formations.Select(m => m.ToModel());
        }
        public IEnumerable<Formations> GetAll(int limit, int offset)
        {
            return _db.Formations.Skip(offset).Take(limit).Select(m => m.ToModel());
        }

        public override bool Add(Formations Formation)
        {
            FormationEntity toInsert = Formation.ToEntity();
            toInsert.fid = GetAll().Count() + 1;
            _db.Formations.Add(toInsert);

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                //_db.Formations.Remove(toInsert);
                return false;
            }
        }

        public override bool Update(Formations Formation)
        {
            FormationEntity toUpdate = _db.Formations.Find(Formation.fid)!;
            toUpdate.fid = Formation.fid;
            _db.Formations.Remove(_db.Formations.Find(Formation.fid)!);
            toUpdate = Formation.ToEntity();
            _db.Formations.Add(toUpdate);
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
            FormationEntity toDelete = _db.Formations.Find(id)!;
            if (toDelete != null)
            {
                try
                {
                    _db.Entry(toDelete).State = EntityState.Modified;
                    _db.Formations.Remove(toDelete);
                    _db.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    _db.Entry(toDelete).State = EntityState.Deleted;
                    // _db.SaveChanges();
                    return false;
                }
            }
            return true;
        }
    }
}