using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SongsAPI.Models.Users;

namespace SongsAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GenerateJwtToken(Student student)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id),
                new Claim(ClaimTypes.Name, student.Name),
                new Claim(ClaimTypes.Email, student.Email ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JwtSettings:SecretKey"] ??
                throw new InvalidOperationException("JWT Secret Key not found in configuration")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(
                Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"] ?? "60"));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
