using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace SGWebAPI.Controllers
{
    [Route("api")]
    public class ValuesController : Controller
    {
        private string communicationKey = "GQDstc21ewfffffffffffFiwDffVvVBrk"; //test string uses to create a key
        [HttpGet("Login")]
        public IActionResult Login()
        {

            string name = "test"; // hard-coded login and passwort to test JWT
            string password = "test";


            if (name == "test" && password == "test")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, name)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(communicationKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
            return BadRequest("Wrong login or password!");
        }


    }
}
