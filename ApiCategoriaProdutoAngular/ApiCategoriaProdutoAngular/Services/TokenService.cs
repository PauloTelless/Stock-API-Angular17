using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiCategoriaProdutoAngular.Services;

public class TokenService : ITokenService
{
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims, IConfiguration _config)
    {
        var key = _config.GetSection("JWT").GetValue<string>("SecretKey") ?? throw new InvalidOperationException("Error Key");

        var privateKey = Encoding.UTF8.GetBytes(key);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(privateKey), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_config.GetSection("JWT")
                                                 .GetValue<double>("TokenValidityInMinutes")),
            Audience = _config.GetSection("JWT").GetValue<string>("ValidAudience"),
            Issuer = _config.GetSection("JWT").GetValue<string>("ValidIssuer"),
            SigningCredentials = signingCredentials,    
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescription);

        return token;
    }

    public string GenerateRefrestToken()    
    {
        var secureRandomByte = new byte[128];

        using var randomNumberGenerator = RandomNumberGenerator.Create();

        randomNumberGenerator.GetBytes(secureRandomByte);       

        var refreshToken = Convert.ToBase64String(secureRandomByte);    

        return refreshToken;    
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _configuration)
    {
        var secretKey = _configuration["JWT:SecretKey"] ?? throw new InvalidOperationException("Invalid Key");

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken 
            || !jwtSecurityToken
            .Header
            .Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid Token");
        }

        return principal;
    }
}
