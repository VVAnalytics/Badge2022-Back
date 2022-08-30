using Badge2022EF.DAL.Entities;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Badge2022EF.DAL
{
    public partial class ExamenEntity 
    { 
        public int eid { get; set; } = int.MinValue;
        public string enom { get; set; } = string.Empty;
        public int enote { get; set; } = int.MinValue;
        public virtual CoursEntity? eCours { get; set; }
    }
}
