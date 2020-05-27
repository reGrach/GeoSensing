using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Auth;
using GeoService.API.Auth.Identity;
using GeoService.API.Models;
using static GeoService.BLL.Actions.UserActions;
using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GeoService.API.Auth.Identity.Contracts;


namespace GeoService.API.Controllers
{
    public class ProfileController : BaseApiController
    {
        private readonly GeoContext _context;

        public ProfileController(GeoContext context)
        {
            _context = context;
        }

        /// <summary> Получить профиль пользователя по id </summary>
        [HttpGet("{id:int}")]
        [Authorize(AdminPolicy)]
        public IActionResult GetById(int id) => TryAction(() => Ok(_context.GetProfile(id)));

        /// <summary> Получить профиль авторизованного пользователя </summary>
        [HttpGet]
        public IActionResult Get() => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var profile = _context.GetProfile(id);
            return Ok(profile);
        });


        /// <summary> Получить список пользователей </summary>
        [HttpGet]
        public IActionResult Search([FromQuery] string query) => TryAction(() => Ok(_context.GetFreeUsers(query)));


        /// <summary> Обновить профиль авторизованного пользователя </summary>
        [HttpPost]
        public IActionResult Update(ProfileModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var user = new UserDTO
            {
                Name = model.Name,
                SurName = model.SurName,
            };
            _context.UpdateProfile(id, user);
            return Ok();
        });
    }
}