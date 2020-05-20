using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.BLL.Actions;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using static GeoService.API.Auth.Identity.Contracts;

namespace GeoService.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class TestController : BaseApiController
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly GeoContext _ctx;

        private const string login = "German";
        private const string password = "12345";


        public TestController(GeoContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _ctx = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpGet]
        [Authorize(AdminPolicy)]
        public IActionResult CreateTeam() => TryAction(() =>
        {
            var team = new Team
            {
                Title = "Тестовая команда 4",
                Color = "Цвет 4"
            };
            _ctx.Teams.Add(team);
            _ctx.SaveChanges();
            return Ok();
        });

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignUp() => TryAction(() =>
        {
            UserActions.TryRegisterUser(_ctx, login, password);
            return Ok();
        });

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn() => TryAction(() =>
        {
            var userDTO = UserActions.AuthenticationUser(_ctx, login, password);

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

            return Ok(tokenResult.AccessToken);

        });

        [HttpGet]
        public IActionResult SignOut() => TryAction(() =>
        {
            HttpContext.Response.Cookies.Delete(".Core.Geo.Bear");
            return Ok();
        });
    }
}