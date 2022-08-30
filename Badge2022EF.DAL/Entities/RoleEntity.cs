using Badge2022EF.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace Badge2022EF.DAL
{
    public class RoleEntity : IdentityRole<int>
    {
        public override int Id { get; set; } = int.MinValue;
        public override string Name { get; set; } = string.Empty;
        public virtual ICollection<PersonneEntity>? Personnes { get; set; }
    }
}
