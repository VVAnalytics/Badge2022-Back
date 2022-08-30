using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
{
    public static class RoleMapper
    {
        public static Roles ToModel(this RoleEntity Entity)
        {
            Roles Role = new(Entity.Name)
            {
                Id = Entity.Id,
                Name = Entity.Name ?? ""
            };
            return Role;
        }

        public static RoleEntity ToEntity(this Roles Model)
        {
            return new RoleEntity()
            {
                Id = Model.Id,
                Name = Model.Name,
            };
        }
    }
}

