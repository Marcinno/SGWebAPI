using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SGWebAPI.Controllers
{
    [Route("api/task")]
    public class TaskController : Controller //feature controller
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("test")]
        public IActionResult get()
        {
            return Ok("Encoded string from other controller"); 
        }
    }
}