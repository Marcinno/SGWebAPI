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
using SGWebAPI.Contracts.Models;

namespace SGWebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult login([FromBody] User model)
        {
            if (model.name == "Testowy1!" && model.password == "Testowy1!")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.name)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    audience: _configuration["Token:Audience"], 
                    issuer: _configuration["Token:Issuer"],
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

        [HttpGet("test")]
        public IActionResult test() // Test method to check JWT works correctly
        {
            return Ok("Jak się włamiesz to kurwa to zobaczysz, #Hackerman");  
        }


    }
}