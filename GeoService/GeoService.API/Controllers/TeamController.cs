using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Auth.Identity;
using GeoService.API.Models;
using GeoService.BLL.Actions;
using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GeoService.API.Auth.Identity.Contracts;
using static GeoService.BLL.Actions.TeamActions;



namespace GeoService.API.Controllers
{
    [Authorize(ParticipantPolicy)]
    public class TeamController : BaseApiController
    {
        private readonly GeoContext _context;

        public TeamController(GeoContext context)
        {
            _context = context;
        }

        /// <summary> Получение всех существующих команд </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() => TryAction(() => Ok(_context.GetAllTeams()));

        /// <summary> Получение полной инфомации о команде </summary>
        /// <param name="id">Идентификатор команды</param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult GetById(int id) => TryAction(() => Ok(_context.GetTeamById(id)));

        /// <summary>
        /// Создание команды.
        /// При этом текущий пользователь автоматически становится ее лидером.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(TeamModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.CreateTeam(id, model.Title, model.Color);
            return Ok();
        });

        /// <summary>
        /// Обновление данных команды.
        /// Текущий пользователь должен быть лидером.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(LeaderPolicy)]
        public IActionResult Update(TeamModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.UpdateTeamByLeader(id, model.Title, model.Color);
            return Ok();
        });


        /// <summary>
        /// Добавление текущего авторизованного пользователя в команду
        /// </summary>
        /// <param name="idTeam"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddMe(int idTeam) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.AddUserToTeam(id, idTeam);
            return Ok(_context.GetTeamById(idTeam));
        });

        [HttpPost]
        [Authorize(LeaderPolicy)]
        public IActionResult AddUser(UserInTeam model) => TryAction(() =>
        {
            _context.AddUserToTeam(model.UserId, model.TeamId);
            return Ok(_context.GetTeamById(model.TeamId));
        });

    }
}