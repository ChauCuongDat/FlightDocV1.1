using FlightDocV1._1.CRUD;
using FlightDocV1._1.Data;
using FlightDocV1._1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightDocV1._1.Controllers
{
    public class TokenController : Controller
    {
        private readonly FlightDocContext _context;
        Query _query;
        Crud _crud;
        public TokenController(FlightDocContext docContext)
        {
            this._context = docContext;
            _query = new Query(docContext);
            _crud = new Crud(docContext);
        }

        [HttpGet]
        [Route("Token")]
        public string Token(UserSection userSection)
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
                Expires = DateTime.UtcNow.AddMinutes(30),
                Subject = claimsPrincipal.Identity as ClaimsIdentity
            };

            var JSTokenHandler = new JwtSecurityTokenHandler();
            var JWT = JSTokenHandler.CreateToken(Description);
            var JWTString = JSTokenHandler.WriteToken(JWT);

            return JWTString;
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(string email, string password)
        {
            User user = new User();
            user.Email = email;
            user.Password = password;
            _crud.AddUser(user);
            return Ok();
        }
        [HttpPost]
        [Route("AddUserSection")]
        public IActionResult AddUserSection (int UserID, string Name)
        {
            var userSection = new UserSection();
            userSection.UserID = UserID;
            _crud.AddUserSection(userSection, Name);
            return Ok();
        }
        [HttpPost]
        [Route("AddRole")]
        public IActionResult AddRole(string Rolename)
        {
            Role role = new Role();
            role.Name = Rolename;
            _crud.AddRole(role);
            return Ok();
        }
        [HttpPatch]
        [Route("AssignUserSection")]
        public IActionResult AssignUserSection(int userId, int userSectionId)
        {
            _query.AssignUserSection(userId, userSectionId);
            return Ok();
        }

        [HttpPatch]
        [Route("AssignRole")]
        public IActionResult AssignRole(int roleId, int userSectionId)
        {
            _query.AssignRole(roleId, userSectionId);
            return Ok();
        }

        [HttpGet]
        [Route("Login")]
        public string Login(string email, string password)
        {
            User user = _context.Users.FirstOrDefault(i => i.Email == email && i.Password == password);
            if (user == null)
            {
                return "Email or password incorrect";
            }
            UserSection userSection = _context.UserSections.FirstOrDefault(i => i.UserID == user.Id);
            if (userSection == null)
            {
                return "No user section has been assign to you";
            }
            return Token(userSection);
        }
    }
}
