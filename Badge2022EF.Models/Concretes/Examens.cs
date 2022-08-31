using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.Models.Concretes
    {
    /// <summary>
    /// </summary>
    public class Examens : IExamens
        {
        public Examens(int Eid, string Enom, int Enote)
            {
            eid = Eid;
            enom = Enom;
            enote = Enote;
            }
        public int eid { get; set; } = int.MinValue;
        public string enom { get; set; } = string.Empty;
        public int enote { get; set; } = int.MinValue;
        public Cours? eCours { get; set; }
    }
    }