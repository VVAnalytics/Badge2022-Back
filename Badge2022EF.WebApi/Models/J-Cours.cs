using Badge2022EF.DAL;
using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_Cours : ICours
    {
        public int cid { get; set; } = int.MinValue;
        public string cnom { get; set; } = string.Empty;
        //public virtual ICollection<ExamenEntity>? cexams { get; set; }
        //public FormationEntity? cform { get; set; }
        //public virtual ICollection<NotesEleveEntity>? cNotesEleve { get; set; }
    }
}
