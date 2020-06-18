using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GeoService.API.Auth.Identity.Contracts;
using static GeoService.BLL.Actions.GeoActions;

namespace GeoService.API.Controllers
{
    [Authorize(ParticipantPolicy)]
    public class GeoController : BaseApiController
    {
        private readonly GeoContext _context;

        public GeoController(JwtTokenGenerator jwtTokenGenerator, GeoContext context) : base(jwtTokenGenerator)
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