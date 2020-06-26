using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.API.Models;
using GeoService.BLL.Actions;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GeoService.API.Auth.Identity.Contracts;
using static GeoService.BLL.Actions.TeamActions;

namespace GeoService.API.Controllers
{
    [Authorize(NonDefinedPolicy)]
    public class TeamController : BaseApiController
    {
        public TeamController(JwtTokenGenerator jwtTokenGenerator, GeoContext context) : base(jwtTokenGenerator, context) { }

        #region Действия админа

        /// <summary> Получение всех существующих команд (для админов) </summary>
        [HttpGet]
        [Authorize(AdminPolicy)]
        public IActionResult GetAll() => TryAction(() => Ok(_context.GetTeams(false)));


        /// <summary> Получение полной инфомации о команде (для админа) </summary>
        /// <param name="id">Идентификатор команды</param>
        [HttpGet("{id:int}")]
        [Authorize(AdminPolicy)]
        public IActionResult GetById(int id) => TryAction(() => Ok(_context.GetTeamById(id)));


        /// <summary> Активировать/деактивировать команду (для админа) </summary>
        [HttpPost]
        [Authorize(AdminPolicy)]
        public IActionResult ChangeActive([Bind("TeamId")] UserInTeam model) => TryAction(() =>
        {
            _context.ChangeActiveTeam(model.TeamId);
            return Ok();
        });

        #endregion


        #region Действия лидера

        /// <summary> Обновление данных команды (для лидера) </summary>
        [HttpPost]
        [Authorize(LeaderPolicy)]
        public IActionResult Update(TeamModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.UpdateTeamByLeader(id, model.Title, model.Color);
            return Ok();
        });

        /// <summary> Выйти из команды </summary>
        [HttpPost]
        [Authorize(LeaderPolicy)]
        public IActionResult RemoveUser([Bind("UserId")] UserInTeam model) => TryAction(() =>
        {
            _context.RemoveUserFromTeam(model.UserId);
            return Ok();
        });

        #endregion


        #region Действия участника

        /// <summary> Получение полной инфомации о собственной команде </summary>
        [HttpGet]
        [Authorize(ParticipantPolicy)]
        public IActionResult GetMy() => TryAction(() =>
        {
            var userId = User.Identity.GetUserId();
            var team = _context.GetTeamByUser(userId);
            return Ok(team);
        });

        /// <summary> Выйти из команды </summary>
        [HttpPost]
        [Authorize(ParticipantPolicy)]
        public IActionResult RemoveMe() => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.RemoveUserFromTeam(id);
            UpdateClaimsAndToken(id, role: RoleEnum.NonDefined);
            return Ok();
        });

        #endregion


        #region Действия пользователя

        /// <summary> Получение всех активных команд </summary>
        [HttpGet]
        public IActionResult GetActive() => TryAction(() => Ok(_context.GetTeams()));

        /// <summary>
        /// Создание команды. Текущий пользователь автоматически становится ее лидером.
        /// </summary>
        [HttpPost]
        public IActionResult Create(TeamModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.CreateTeam(id, model.Title, model.Color);
            UpdateClaimsAndToken(id, role: RoleEnum.Leader);
            return Ok();
        });

        /// <summary> Добавление текущего авторизованного пользователя в команду </summary>
        [HttpPost]
        public IActionResult AddMe([Bind("TeamId")] UserInTeam model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            _context.AddUserToTeam(id, model.TeamId);
            UpdateClaimsAndToken(id, role: RoleEnum.Participant);
            return Ok();
        });

        #endregion
    }
}