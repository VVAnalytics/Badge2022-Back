using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_Formations : IFormations
    {
        public int fid { get; set; } = int.MinValue;
        public string fnom { get; set; } = string.Empty;
        //public virtual ICollection<PersonneEntity>? fPersonnes { get; set; }
        //public virtual ICollection<CoursEntity>? fCours { get; set; }
    }
}
