using Badge2022EF.Models.Concretes;
using Badge2022EF.WebApi.Models;

namespace Badge2022EF.WebApi.JWT_Authentication.JWTWebAuthentication.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(J_Users users);
    }

}
