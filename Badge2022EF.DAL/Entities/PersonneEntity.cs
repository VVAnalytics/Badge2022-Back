using Microsoft.AspNetCore.Identity;

namespace Badge2022EF.DAL.Entities
{
    public partial class PersonneEntity : IdentityUser<int>
    {
        public string? unom { get; set; }
        public string? uprenom { get; set; }

        public override string? Email { get; set; }

        public string? udate { get; set; }
        public string? urue { get; set; }
        public string? ucodep { get; set; }
        public string? uville { get; set; }
        public string? upays { get; set; }

        public virtual ICollection<RoleEntity>? urole { get; set; }
        public FormationEntity? uformation { get; set; }
        public virtual ICollection<NotesEleveEntity>? uNotesEleve { get; set; }

    }
}
