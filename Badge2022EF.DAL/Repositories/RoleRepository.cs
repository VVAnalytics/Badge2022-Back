using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL.Repositories.Abstracts;
using Badge2022EF.DAL.Repositories.Interface;
using Badge2022EF.DAL.Repositories.Mappers;
using Badge2022EF.Models.Concretes;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Collections.ObjectModel;

namespace Badge2022EF.DAL.Repositories
{
    public class RoleRepository : BaseRepository<Roles>, IRoleRepository
    {
        private readonly UserManager<PersonneEntity> _userManager;

        public RoleRepository(
            UserManager<PersonneEntity> userManager,
            Badge2022Context context) : base(context)
        {
            _userManager = userManager;
        }

        public override bool Add(Roles Role)
        {
            RoleEntity toInsert = Role.ToEntity();
            toInsert.Id = GetAll().Count()+1;
            _db.Roles.Add(toInsert);

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                //_db.Roles.Remove(toInsert);
                return false;
            }
        }

        public override bool Delete(int id)
        {
            RoleEntity toDelete = _db.Roles.Find(id)!;
            if (toDelete != null)
            {
                try
                {
                    _db.Entry(toDelete).State = EntityState.Modified;
                    _db.Roles.Remove(toDelete);
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
            RoleEntity toUpdate = new();
            toUpdate = _db.Roles.Find(Role.Id)!;
            if (toUpdate != null)
            {
                toUpdate.Id = Role.Id;
                toUpdate.Name = Role.Name;
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
            return true;
        }
    }
}
