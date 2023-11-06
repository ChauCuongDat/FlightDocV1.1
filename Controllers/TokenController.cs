using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocV1._1.Controllers
{
    public class TokenController : Controller
    {
        [HttpPost]
        [Route("Token")]
        public IActionResult AdminToken()
        {
            var claims = new List<Claim>
            {
                new Claim("Id","0")
            };
            var claimsIdentity = new ClaimsIdentity(claims, "MyAuthenticateType");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var Description = new SecurityTokenDescriptor
            {
                Issuer = "TokenIssuer",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("The Super Duper Extreamly Mystery Key")),
                    SecurityAlgorithms.HmacSha256Signature),
                Audience = "TokenUser",
                Expires = DateTime.UtcNow.AddSeconds(360),
                Subject = claimsPrincipal.Identity as ClaimsIdentity
            };

            var JSTokenHandler = new JwtSecurityTokenHandler();
            var JWT = JSTokenHandler.CreateToken(Description);
            var JWTString = JSTokenHandler.WriteToken(JWT);

            return Ok(JWTString);
        }
    }
}
