using AutoMapper;
using GeoService.API.Auth.Identity;
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
        public GeoController(GeoContext context, IMapper mapper) : base(context, mapper) { }


        /// <summary> Получить список точек для команды по id</summary>
        [Authorize(AdminPolicy)]
        [HttpGet]
        public IActionResult GetPoints(int id) => TryAction(() =>
        {
            return Ok();
        });


        #region Действия участника

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

        /// <summary> Получить список точек для текущей команды </summary>
        [HttpGet]
        public IActionResult GetMyPoints() => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var points = _context.GetPoints(id);
            return Ok(points);
        });

        /// <summary> Зафиксировать точку </summary>
        [HttpPost]
        public IActionResult FixationPoint(FixPointModel model) => TryAction(() =>
        {
            var parametrs = _mapper.Map<SavePointDTO>(model);
            _context.AddPoint(parametrs, User.Identity.GetUserId());
            return Ok();
        });

        #endregion
    }
}