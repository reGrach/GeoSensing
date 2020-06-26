using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.API.Models;
using GeoService.BLL.DTO;
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
        [HttpPost]
        public IActionResult InitExperiment(string title) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var expTitle = _context.InitExperiment(id, title);
            return Ok(expTitle);
        });

        /// <summary> Закончить эксперимент </summary>
        [HttpPost]
        public IActionResult CloseExperiment(int ExpId) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.CloseExperiment(id, ExpId);
            return Ok();
        });

        /// <summary> Получить список экспериментов текущей команды </summary>
        [HttpGet]
        public IActionResult GetMyExperiments() => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var expirements = _context.GetExperimentsByUserId(id);
            return Ok(expirements);
        });

        /// <summary> Зафиксировать точку </summary>
        [HttpPost]
        public IActionResult FixationPoint(FixPointModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var dto = new GeoParameterDTO
            {
                Accuracy = model.Accuracy,
                Altitude = model.Altitude,
                AltitudeAccuracy = model.AltitudeAccuracy,
                Speed = model.Speed,
                CreateTime = model.CreateTime,
                ExperimentId = model.ExperimentId,
                Heading = model.Heading,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Mode = model.Mode,
                UserId = id
            };
            _context.AddPoint(dto);
            return Ok();
        });
    }
}