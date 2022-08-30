using Badge2022EF.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Badge2022EF.DAL
{
    public class ExamenEntity : IdentityRole<int>
    {
        public override int Id { get; set; } = int.MinValue;
        public override string Name { get; set; } = string.Empty;
        public ICollection<PersonneEntity>? Personnes { get; set; }
    }
}
