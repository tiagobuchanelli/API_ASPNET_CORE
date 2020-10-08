using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lojax.Models;
using Microsoft.IdentityModel.Tokens;

namespace Lojax.Services
{//Classe estatica não precisa instanciar (ex: new TokenService())
    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                   //O Claim tem tipos como Nome e Role, e podemos associar ao modelo de usuario caso tenha esses campos ou algum parecido.
                   //new Claim(ClaimTypes.Name, user.Username.ToString()),
                   new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) //Token gerado baseado no algoritimo de encriptação
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}