using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.Models.Concretes
{
    /// <summary>
    /// </summary>
    public class NotesEleves : INotesEleves
        {
  
        public NotesEleves(int nnote)
        {
            this.nnote = nnote;
        }

        public NotesEleves(int Npid, int Ncid, int Nnote )
            {
            npid = Npid;
            ncid = Ncid;
            nnote = Nnote;
            }
        #region Fields

        public int npid { get; set; } = int.MinValue;
        public Personnes Personnes { get; set; }

        public int ncid { get; set; } = int.MinValue;
        public Cours Cours { get; set; }

        public int nnote { get; set; } = int.MinValue;

        #endregion
    }
    }
