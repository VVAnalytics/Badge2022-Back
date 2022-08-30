using Badge2022EF.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Badge2022EF.DAL.Entities
{
    public partial class PersonneEntity : IdentityUser<long>
    {
        public override string? Email { get; set; }

        public string? rue { get; set; }
        public string? codep { get; set; }
        public string? ville { get; set; }
        public string? pays { get; set; }

        public virtual ICollection<FormationEntity>? Formations { get; set; }

        public virtual ICollection<RoleEntity>? Roles { get; set; }
    }
}
