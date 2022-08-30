using Badge2022EF.Models.Interfaces;
namespace Badge2022EF.WebApi.Models
{
    public class J_Roles : IRoles
    {
        public long Id { get; set; } = long.MinValue;
        public string Name { get; set; } = string.Empty;
    }
}
