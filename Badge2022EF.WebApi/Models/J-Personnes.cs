using Badge2022EF.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Badge2022EF.WebApi.Models
{
    public class J_Personnes : IdentityUser
    {
        public string? unom { get; set; } = string.Empty;
        public string? uprenom { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public DateTime? udate { get; set; } = DateTime.MinValue;
        public string? urue { get; set; } = string.Empty;
        public string? ucodep { get; set; } = string.Empty;
        public string? uville { get; set; } = string.Empty;
        public string? upays { get; set; } = string.Empty;

    }
}
