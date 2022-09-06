using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Concretes;

namespace Badge2022EF.DAL.Repositories.Mappers
{
    public static class RoleMapper
    {
        public static Roles ToModel(this RoleEntity? Entity)
        {
            if (Entity != null) {
                Roles Role = new(Entity?.Name ?? string.Empty)
                {
                    Id = Entity.Id,
                    Name = Entity?.Name ?? string.Empty
                };
                return Role;
            }
            return null;
        }

        public static RoleEntity ToEntity(this Roles Model)
        {
            return new RoleEntity()
            {
                Id = Model.Id,
                Name = Model.Name,
                NormalizedName = Model.Name.ToUpperInvariant(),
            };
        }
    }
}

