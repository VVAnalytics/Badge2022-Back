using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.DAL.Repositories
{
    public class RoleRepository : BaseRepository<Roles>, IRoleRepository
    {
        public RoleRepository(Badge2022Context context) : base(context)
        {
        }

        public override bool Add(Roles Role)
        {
            RoleEntity toInsert = Role.ToEntity();
            _db.Roles.Add(toInsert);

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                _db.Roles.Remove(toInsert);
                return false;
            }
        }

        public override bool Delete(long id)
        {
            try
            {
                _db.Roles.Remove(_db.Roles.Find(id)!);
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public override IEnumerable<Roles> GetAll()
        {
            return _db.Roles.Select(m => m.ToModel());
        }
        public IEnumerable<Roles> GetAll(int limit, int offset)
        {
            return _db.Roles.Skip(offset).Take(limit).Select(m => m.ToModel());
        }
        public override Roles GetOne(int id)
        {
            return _db.Roles.Find(id)!.ToModel();
        }
        public override IEnumerable<Roles> GetOne2(int id)
        {
            yield return _db.Roles.Find(id)!.ToModel();
        }

        public override bool Update(Roles Role)
        {
            RoleEntity toUpdate = _db.Roles.Find(Role.Id)!;
            toUpdate.Id = Role.Id;
            _db.Roles.Remove(_db.Roles.Find(Role.Id)!);
            toUpdate = Role.ToEntity();
            _db.Roles.Add(toUpdate);

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
    }
}
