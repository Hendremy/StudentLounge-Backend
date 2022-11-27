using Microsoft.AspNetCore.Identity;
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

        public string Create(AppUser user, IEnumerable<string> roles)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512);
            var claims = CreateClaims(user, roles);

            var token = new JwtSecurityToken(issuer: _issuer, audience: _audience, claims: claims,
                expires: DateTime.Now.AddDays(365), signingCredentials: creds);
            //TODO: Réduire la durée d'un token à moins de 24h (+ setup refresh token)
            //Durée de 365 jours pour pouvoir tester sans refresh à chaque fois

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private IEnumerable<Claim> CreateClaims(AppUser user, IEnumerable<string> roles)
        {
            IList<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.Fullname)
            };
            AddRoleClaims(claims, roles);
            return claims;
        }

        private void AddRoleClaims(ICollection<Claim> claims, IEnumerable<string> roles)
        {
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }
    }

}
