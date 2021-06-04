using ELibrary.Core.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELibrary.Core.Implementations
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public string GenerateToken(string username,
            string userId, string email, IConfiguration config, string[] roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId)

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(config.GetSection("JWT:JWTSigningKey").Value)),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCreated = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(tokenCreated);
        }
    }
}
