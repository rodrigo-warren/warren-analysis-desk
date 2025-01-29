using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace warren_analysis_desk
{
    [ApiController]
    [Route("api/auth-token")]
    public class TokenController : ControllerBase
    {
        private readonly string chaveSecreta;
        private readonly string issuer;
        private readonly string audience;
        private readonly string AdmUsername;
        private readonly string AdmPassword;

        public TokenController(IConfiguration configuration)
        {
            issuer = configuration["Jwt:Issuer"];
            audience = configuration["Jwt:Audience"];
            chaveSecreta = configuration["Jwt:Key"];
            AdmUsername = configuration["Adm:Username"];
            AdmPassword = configuration["Adm:Password"];
        }

        [HttpPost]
        public ActionResult<string> GetToken(AdmUser admUser)
        {
            if(
                admUser.Username == AdmUsername && 
                admUser.Password == AdmPassword) 
            {
                var tokenJwt = GerarTokenJwt(admUser.Username); 
                return Ok(tokenJwt);
            }

            return BadRequest("Administrative credentials incorrect."); 
        }

        private string GerarTokenJwt(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new System.Security.Claims.Claim[]
            {
                new System.Security.Claims.Claim("username", username),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}