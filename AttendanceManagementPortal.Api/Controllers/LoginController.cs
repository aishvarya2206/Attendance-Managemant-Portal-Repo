using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AttendanceManagementPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IConfiguration _config;
       
        
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
            
        }
       
        /* private static Users AuthenticateUser(Users user)
         {
             Users _user = null;
             if (user.Username == "admin" && user.Password == "1234")
             {
                 _user = new Users { Username = "Aishvarya" };
             }
             return _user;
         }*/
        /*private string GenerateToken(Users user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(2), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }*/
        /* [AllowAnonymous]
         [HttpPost]
         public IActionResult Login(Users users)
         {
             IActionResult response = Unauthorized();
             var user = AuthenticateUser(users);
             if (user != null)
             {
                 var token = GenerateToken(user);
                 response = Ok(new { token = token });
             }
             return response;
         }*/

    }
}
