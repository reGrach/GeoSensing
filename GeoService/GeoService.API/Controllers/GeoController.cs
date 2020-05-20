using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Auth.Identity;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GeoService.BLL.Actions.GeoActions;
using static GeoService.API.Auth.Identity.Contracts;


namespace GeoService.API.Controllers
{
    [Authorize(ParticipantPolicy)]
    public class GeoController : BaseApiController
    {
        private readonly GeoContext _context;

        public GeoController(GeoContext context)
        {
            _context = context;
        }

        /// <summary> Инициировать эксперимент </summary>
        [HttpGet]
        public IActionResult InitExperiment([FromBody] string title) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var expTitle = _context.InitExperiment(id, title);
            return Ok(expTitle);
        });
    }
}