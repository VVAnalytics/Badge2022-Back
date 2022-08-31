using Badge2022EF.Models.Interfaces;
using System.Security.Cryptography;

namespace Badge2022EF.Models.Concretes
    {
    /// <summary>
    /// </summary>
    public class Cours : ICours
        {
        public Cours(int Cid, string Cnom, long Armo_Patient)
            {
            cid = Cid;
            cnom = Cnom;
            }
        public int cid { get; set; } = int.MinValue;
        public string cnom { get; set; } = string.Empty;
        }
    }
