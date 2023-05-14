using Badge2022EF.WebApi.JWT_Authentication.JWTWebAuthentication.Repository;
using Badge2022EF.WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Badge2022EF.DAL.Repositories;
using Badge2022EF.DAL.Entities;
using Badge2022EF.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Badge2022EF.WebApi.JWT_Authentication
{
	public class JWTManagerRepository : IJWTManagerRepository
	{
        public JWTManagerRepository()
        {

        }
        public Tokens Authenticate(J_Users users)
		{

			JwtSecurityTokenHandler tokenHandler = new();
			//var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
			byte[] tokenKey = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRETJWT_Badge2022EF", EnvironmentVariableTarget.Machine)??string.Empty);
            if (tokenKey.Length < 1) {
                tokenKey = Encoding.UTF8.GetBytes("oPXzRVyHyAeerCEVjfYLJ5XkiuHWfhhJCgFL1Van+jt4N/oXApi8tK6FaBSmQzmYvdV9mWOiTFy+UXyr7jsTZ/bjcGmR5D6PzdOoUUbeS98PF2e3iakk5Sh+Y9e5y7fSJ1vVUwCsxyA/S0xn1zPGWufCdP6QMkAcUeb0IpzNmYQ=");
            };
            SecurityToken token = new JwtSecurityToken(null, null, new Claim[]
                       {
                          new Claim(ClaimTypes.Name, users.Email??String.Empty),
                          new Claim(ClaimTypes.Role, users.Role??String.Empty)
                       }, DateTime.Now, DateTime.Now.AddDays(1), new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature));
            return new Tokens { Token = tokenHandler.WriteToken(token) };

		}
	}
}
