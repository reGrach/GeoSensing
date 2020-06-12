using GeoService.API.Auth.Identity;
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
                new CookieOptions { MaxAge = tokenResult.Expires });

            var info = new AuthInfoModel
            {
                Login = userDTO.Login,
                Role = userDTO.Role.ToString(),
                Expiration = DateTime.Now.AddHours(tokenResult.Expires.Hours)
            };

            return Ok(info);
        });


        /// <summary> Выход пользователя из системы </summary>
        [HttpPost]
        public IActionResult SignOut() => TryAction(() =>
        {
            HttpContext.Response.Cookies.Delete(".Core.Geo.Bear");
            return Ok();
        });

        /// <summary> Проверка того, что пользователь авторизован, если да - возвращает логин </summary>
        [HttpGet]
        public IActionResult Check() => TryAction(() => Ok(new AuthInfoModel
        {
            Login = User.Identity.GetUserLogin(),
            Role = User.Identity.GetUserRole().ToString(),
            Expiration = User.Identity.GetExpirationToken()
        }));
    }
}