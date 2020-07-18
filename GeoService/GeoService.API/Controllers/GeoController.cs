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

        /// <summary> Зафиксировать точку </summary>
        [HttpPost]
        public IActionResult FixationPoint(FixPointModel model) => TryAction(() =>
        {
            var parametrs = _mapper.Map<GeoParameterDTO>(model);
            parametrs.UserId = User.Identity.GetUserId();
            _context.AddPoint(parametrs);

            return Ok();
        });

        #endregion
    }
}