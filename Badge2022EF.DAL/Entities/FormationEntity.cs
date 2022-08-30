using Badge2022EF.DAL.Entities;

namespace Badge2022EF.DAL
{
    public partial class FormationEntity 
    { 
        public int fid { get; set; } = int.MinValue;
        public string fnom { get; set; } = string.Empty;
        public virtual ICollection<PersonneEntity>? fPersonnes { get; set; }
        public virtual ICollection<CoursEntity>? fCours { get; set; }
    }
}
