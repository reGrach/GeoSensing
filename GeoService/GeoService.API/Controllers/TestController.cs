using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GeoService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private GeoContext _ctx;

        public TestController(GeoContext context)
        {
            _ctx = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var nameIdentifier = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name);

            return new string[] { nameIdentifier?.Value, "value1", "value2" };
        }

        [HttpGet("db")]
        public ActionResult TestDb()
        {
            var team = new Team
            {
                Title = "Привет",
                Color = "Цвет"
            };
            _ctx.Teams.Add(team);
            _ctx.SaveChanges();

            return Ok();
        }
    }
}
