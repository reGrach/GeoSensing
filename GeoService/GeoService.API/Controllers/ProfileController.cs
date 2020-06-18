using GeoService.API.Auth.Identity;
using GeoService.API.Models;
using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using static GeoService.API.Auth.Identity.Contracts;
using static GeoService.BLL.Actions.UserActions;


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
            return Ok(_context.GetProfile(id));
        });

        /// <summary> Загрузка аватара для пользователя </summary>
        [HttpPost]
        public IActionResult UploadAvatar(IFormFile avatar) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            using (var memoryStream = new MemoryStream())
            {
                avatar.CopyTo(memoryStream);
                if (memoryStream.Length < 5000000)
                    _context.CreateUpdateAvatar(id, memoryStream.ToArray(), avatar.ContentType);
            }
            return Ok();
        });
    }
}