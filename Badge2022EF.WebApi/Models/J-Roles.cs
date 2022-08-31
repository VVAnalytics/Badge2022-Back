using Badge2022EF.Models.Interfaces;
namespace Badge2022EF.WebApi.Models
{
    public class J_Roles : IRoles
    {
        public int Id { get; set; } = int.MinValue;
        public string Name { get; set; } = string.Empty;
    }
}
