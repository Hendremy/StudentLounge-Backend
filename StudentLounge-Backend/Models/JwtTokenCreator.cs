using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace StudentLounge_Backend.Models
{
    public class JwtTokenCreator : IJwtTokenCreator
    {
        private readonly SymmetricSecurityKey _key;
        public JwtTokenCreator(string key)
        {
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
        }

        public string CreateToken(StudentLoungeUser user)
        {
            var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken();

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }

}
