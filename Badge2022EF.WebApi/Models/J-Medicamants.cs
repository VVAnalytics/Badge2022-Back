using Badge2022EF.Models.Interfaces;

namespace Badge2022EF.WebApi.Models
{
    public class J_Medicaments : INotesEleves
    {
        public long MedicamentId { get; set; } = long.MinValue;
        public string MedicamentNom { get; set; } = string.Empty;
    }
}
