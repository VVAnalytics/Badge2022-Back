using Badge2022EF.DAL;
using Badge2022EF.DAL.Entities;
using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_Examens : IExamens
    {
        #region Fields

        public int eid { get; set; } = int.MinValue;
        public string enom { get; set; } = string.Empty;
        public int enote { get; set; } = int.MinValue;
        // public virtual CoursEntity? eCours { get; set; }

        #endregion
    }
}
