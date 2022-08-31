using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_NotesEleves : INotesEleves
    {
        public int npid { get; set; } = int.MinValue;
        public virtual PersonneEntity nPersonnes { get; set; }

        public int ncid { get; set; } = int.MinValue;
        public virtual CoursEntity nCours { get; set; }

        public int nnote { get; set; } = int.MinValue;
    }
}
