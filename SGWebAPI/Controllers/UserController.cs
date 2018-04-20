using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace SGWebAPI.Controllers
{
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        [HttpPost("login")]
        public IActionResult login(string name, string password)
        {

            if (name == "test" && password == "test")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, name)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    audience: "yourdomain.com",
                    issuer: "yourdomain.com",
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

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("test")]
        public IActionResult test()
        {
            return Ok("Jak się włamiesz to kurwa to zobaczysz, #Hackerman");
        }


    }
}
