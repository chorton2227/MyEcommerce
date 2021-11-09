namespace MyEcommerce.Services.IdentityService.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyEcommerce.Services.IdentityService.API.Models;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult FakeData()
        {
            return Json(new List<User> {
                new User {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "John Doe",
                    Email = "JohnDoe@example.com"
                },
                new User {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Bob Rogers",
                    Email = "BobRogers@example.com"
                },
                new User {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "Liz Beth",
                    Email = "LizBeth@example.com"
                }
            });
        }
    }
}