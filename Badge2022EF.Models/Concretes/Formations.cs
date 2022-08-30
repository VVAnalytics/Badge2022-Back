using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.Models.Concretes
    {
    /// <summary>
    /// </summary>
    public class Formations : IFormations
        {
        public Formations(int Id, string nom)
            {
            fid = Id;
            fnom = nom;
            }
        #region Fields
        public int fid { get; set; } = int.MinValue;
        public string fnom { get; set; } = string.Empty;
        public ICollection<Personnes>? fPersonnes { get; set; }
        public ICollection<Cours>? fCours { get; set; }
        #endregion // Fields
    }
    }
