using GeoService.API.Auth.Identity;
using GeoService.API.Auth.JwtExtension;
using GeoService.BLL;
using GeoService.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GeoService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public BaseApiController(JwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }


        internal IActionResult TryAction(Func<IActionResult> action)
        {
            IActionResult result;
            try
            {
                result = action.Invoke();
            }
            catch (ApiException ex)
            {
                result = StatusCode(ex.StatusCode, ex.Value);
            }
            catch (Exception ex)
            {
                result = Problem(detail: ex.InnerException.Message, title: ex.Message);
            }
            return result;
        }

        internal void UpdateClaimsAndToken(int userId, string login = null, RoleEnum? role = null)
        {
            var userName = login ?? User.Identity.GetUserLogin();
            var currentRole = role ?? User.Identity.GetUserRole();
            var expiredDate = User.Identity.GetExpirationToken();

            var userIdentity = new UserIdentity
            {
                Id = userId,
                UserName = userName
            };

            var tokenResult = _jwtTokenGenerator.Generate(userIdentity, currentRole.ToString(), expiredDate);

            HttpContext.Response.Cookies.Append(
                ".Core.Geo.Bear",
                tokenResult.AccessToken,
                new CookieOptions { MaxAge = tokenResult.Expires });
        }

        internal DateTime CreateClaimsAndToken(int userId, string login, RoleEnum role, bool rememberMe)
        {
            var userIdentity = new UserIdentity
            {
                Id = userId,
                UserName = login
            };

            var tokenResult = _jwtTokenGenerator.Generate(userIdentity, role.ToString(), rememberMe);

            HttpContext.Response.Cookies.Append(
                ".Core.Geo.Bear",
                tokenResult.AccessToken,
                new CookieOptions { MaxAge = tokenResult.Expires });

            return DateTime.UtcNow.AddHours(tokenResult.Expires.TotalHours);
        }
    }
}