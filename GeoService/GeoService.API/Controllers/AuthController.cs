using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.API.Models;
using GeoService.BLL.Actions;
using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GeoService.API.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly GeoContext _ctx;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthController(JwtTokenGenerator jwtTokenGenerator, GeoContext context)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _ctx = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(RegistrationModel model) => TryAction(() =>
        {
            UserActions.TryRegisterUser(_ctx, model.Login, model.Password);
            return Ok();
        });

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn(LoginModel model) => TryAction(() =>
        {
            if (UserActions.AuthenticationUser(_ctx, model.Login, model.Password) is UserDTO userDTO)
            {
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
            }
            else
                return Unauthorized();
        });

        [HttpPost]
        public IActionResult SignOut() => TryAction(() =>
        {
            HttpContext.Response.Cookies.Delete(".Core.Geo.Bear");
            return Ok();
        });
    }
}