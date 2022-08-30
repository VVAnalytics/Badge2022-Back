using Badge2022EF.DAL.Entities;

namespace Badge2022EF.DAL
{
    public partial class NotesEleveEntity 
    { 
        public int npid { get; set; } = int.MinValue;
        public PersonneEntity Personnes { get; set; }

        public int ncid { get; set; } = int.MinValue;
        public CoursEntity Cours { get; set; }

        public int nnote { get; set; } = int.MinValue;
    }
}
