using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentLounge_Backend.Models.Authentication
{
    public class JwtTokenCreator : ICreateToken
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;
        private readonly string _audience;


        public JwtTokenCreator(string key, string issuer, string audience)
        {
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
            _issuer = issuer;
            _audience = audience;
        }

        public string Create(AppUser user)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Fullname),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = new JwtSecurityToken(_issuer, _audience, claims,
                expires: DateTime.Now.AddDays(1), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
