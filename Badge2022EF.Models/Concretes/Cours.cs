using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.Models.Concretes
    {
    /// <summary>
    /// </summary>
    public class Cours : ICours
        {
        public Cours(long Armo_ID, string Armo_Name, long Armo_Patient)
            {
            ArmoID = Armo_ID;
            ArmoName = Armo_Name;
            ArmoPatient = Armo_Patient;
            }
        public long ArmoID { get; set; } = long.MinValue;
        public string ArmoName { get; set; } = string.Empty;
        public long ArmoPatient { get; set; } = long.MinValue;
    }
    }
