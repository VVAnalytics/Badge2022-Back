using Badge2022EF.DAL.Repositories;
using Badge2022EF.Models.Interfaces;
namespace Badge2022EF.WebApi.Models
{
    public class J_Login
    {
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public J_Login(string e, string p)
        {
            email = e;
            password = p;
        }
        public J_Login() {}
    }
}
