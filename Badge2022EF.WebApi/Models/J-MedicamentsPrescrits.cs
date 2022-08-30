using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_MedicamentsPrescrits : IMedicamentsPrescrits
    {
        public long MPMedicamentId { get; set; } = long.MinValue;
        public long MPOrdonnanceId { get; set; } = long.MinValue;
        public long MPQuantite { get; set; } = long.MinValue;
        public long MPPrise { get; set; } = long.MinValue;
    }
}
