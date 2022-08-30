using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_Armoires : IArmoires
    {
        public long ArmoID { get; set; } = long.MinValue;
        public string ArmoName { get; set; } = string.Empty;
        public long ArmoPatient { get; set; } = long.MinValue;
    }
}
