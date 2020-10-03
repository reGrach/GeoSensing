using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.API.Models;
using GeoService.BLL.Actions;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GeoService.BLL.Actions.UserActions;


namespace GeoService.API.Controllers
{
    public class AuthController : BaseApiController
    {
        public AuthController(JwtTokenGenerator jwtTokenGenerator, GeoContext context) : base(jwtTokenGenerator, context) { }

        /// <summary> Регистрация пользователя </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp(AuthModel model) => TryAction(() =>
        {
            var user = _context.TryRegisterUser(model.Login, model.Password);

            var expirationDate = CreateClaimsAndToken(user.Id, user.Login, user.Role, model.RememberMe);

            var info = new AuthInfoModel
            {
                Login = user.Login,
                Role = user.Role.ToString(),
                Expiration = expirationDate,
                AvatarSrc = _context.GetAvatar(user.Id)
            };

            return Ok(info);
        });


        /// <summary> Аутентификация пользователя </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn(AuthModel model) => TryAction(() =>
        {
            var user = _context.AuthenticationUser(model.Login, model.Password);

            var expirationDate = CreateClaimsAndToken(user.Id, user.Login, user.Role, model.RememberMe);

            var info = new AuthInfoModel
            {
                Login = user.Login,
                Role = user.Role.ToString(),
                Expiration = expirationDate,
                AvatarSrc = _context.GetAvatar(user.Id)
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

        /// <summary> Проверка того, что пользователь авторизован, если да - возвращаем данные </summary>
        [HttpGet]
        public IActionResult Check() => TryAction(() => Ok(new AuthInfoModel
        {
            Login = User.Identity.GetUserLogin(),
            Role = User.Identity.GetUserRole().ToString(),
            AvatarSrc = _context.GetAvatar(User.Identity.GetUserId()),
            Expiration = User.Identity.GetExpirationToken()
        }));



        ///// <summary> Продление жизни токена </summary>
        //[HttpPost]
        //public IActionResult Extend() => TryAction(() =>
        //{
        //    if (User.Identity.GetExpirationToken().Subtract(DateTime.Now).TotalHours is double _h
        //        && _h > 0 && _h < 1)
        //    {

        //    }

        //    return Ok();
        //});
    }
}