using FlightDocV1._1.Data;
using FlightDocV1._1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocV1._1.Controllers
{
    public class TokenController : Controller
    {
        private readonly FlightDocContext _context;
        public TokenController(FlightDocContext docContext)
        {
            this._context = docContext;
        }

        [HttpPost]
        [Route("Token")]
        public IActionResult Token(UserSection userSection)
        {
            Role role = _context.Roles.Find(userSection.RoleID);

            var claims = new List<Claim>
            {
                new Claim("Role",role.Name)
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
