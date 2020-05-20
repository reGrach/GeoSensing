﻿using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.API.Models;
using GeoService.BLL.Actions;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static GeoService.BLL.Actions.UserActions;


namespace GeoService.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly GeoContext _context;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(JwtTokenGenerator jwtTokenGenerator, GeoContext context)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        /// <summary> Регистрация пользователя </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(AuthModel model) => TryAction(() =>
        {
            _context.TryRegisterUser(model.Login, model.Password);
            return Ok();
        });


        /// <summary> Аутентификация пользователя </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn(AuthModel model) => TryAction(() =>
        {
            var userDTO = _context.AuthenticationUser(model.Login, model.Password);
            var userIdentity = new UserIdentity
            {
                Id = userDTO.Id,
                UserName = userDTO.Login
            };

            var tokenResult = _jwtTokenGenerator.Generate(userIdentity, userDTO.Role.ToString());

            HttpContext.Response.Cookies.Append(
                ".Core.Geo.Bear",
                tokenResult.AccessToken,
                new CookieOptions { MaxAge = TimeSpan.FromMinutes(60) });
            return Ok();
        });


        /// <summary> Выход пользователя из системы </summary>
        [HttpPost]
        public IActionResult SignOut() => TryAction(() =>
        {
            HttpContext.Response.Cookies.Delete(".Core.Geo.Bear");
            return Ok();
        });
    }
}