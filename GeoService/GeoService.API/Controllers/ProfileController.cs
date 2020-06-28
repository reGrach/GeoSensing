﻿using AutoMapper;
using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.API.Models;
using GeoService.BLL;
using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using static GeoService.API.Auth.Identity.Contracts;
using static GeoService.BLL.Actions.UserActions;

namespace GeoService.API.Controllers
{
    public class ProfileController : BaseApiController
    {        
        public ProfileController(JwtTokenGenerator jwtTokenGenerator, GeoContext context, IMapper mapper) : base(jwtTokenGenerator, context, mapper) { }

        #region Действия админа

        /// <summary> Получить профиль пользователя по id </summary>
        [HttpGet("{id:int}")]
        [Authorize(AdminPolicy)]
        public IActionResult GetById(int id) => TryAction(() => Ok(_context.GetProfile(id)));

        /// <summary> Получить список пользователей </summary>
        [HttpGet]
        [Authorize(AdminPolicy)]
        public IActionResult Search(string query, string role) => TryAction(() => Ok(_context.GetFilterUsers(query, role)));

        #endregion

        #region Действия пользователя

        /// <summary> Получить профиль текущего пользователя </summary>
        [HttpGet]
        public IActionResult Get() => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var profile = _context.GetProfile(id);
            return Ok(profile);
        });

        /// <summary> Обновить профиль авторизованного пользователя </summary>
        [HttpPost]
        public IActionResult Update(ProfileModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var user = _mapper.Map<UserDTO>(model);
            var updateUser = _context.UpdateProfile(id, user);

            return Ok(updateUser);
        });

        /// <summary> Загрузка аватара для пользователя </summary>
        [HttpPost]
        public IActionResult UploadAvatar(IFormFile avatar) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            using (var memoryStream = new MemoryStream())
            {
                avatar.CopyTo(memoryStream);
                if (memoryStream.Length < 3145728)
                    _context.CreateUpdateAvatar(id, memoryStream.ToArray(), avatar.ContentType);
                else
                    throw new ApiException("Размер файла не должен превышать 3 МБ", nameof(UploadAvatar), 400);
            }
            return Ok();
        });

        #endregion
    }
}