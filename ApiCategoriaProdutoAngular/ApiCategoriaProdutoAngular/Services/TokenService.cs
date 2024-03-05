using ApiCategoriaProdutoAngular.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiCategoriaProdutoAngular.Services
{
    public class TokenService
    {
        public SecurityTokenDescriptor tokenDescriptor;

        public string GenereteToken(AuthUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.Configuration.PrivateKey);

            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                algorithm: SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClams(user),
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2)
            };

            var token = handler.CreateToken(tokenDescriptor);

            return handler.WriteToken(token);

        }

        private static ClaimsIdentity GenerateClams(AuthUser user)
        {
            var ci = new ClaimsIdentity();

            ci.AddClaim(new Claim(type: ClaimTypes.Name, value: user.UsuarioId.ToString()));

            return ci;
        }
    }
}
