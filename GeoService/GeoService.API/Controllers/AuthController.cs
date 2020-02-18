using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoService.API.Auth;
using GeoService.API.BusinessLogic;
using GeoService.API.Models;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace GeoService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IOptions<AuthOptions> authOptions;
        private readonly GeoContext _ctx;
        public AuthController(GeoContext context, IOptions<AuthOptions> config)
        {
            authOptions = config;
            _ctx = context;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model, [FromServices] IJwtSigningEncodingKey signingKey)
        {
            try
            {
                var identity = GetIdentity(model);
                if (identity == null)
                    return BadRequest(new { errorText = "Логин или пароль указаны неверно" });

                // создаем JWT-токен
                var jwt = new JwtSecurityToken(
                        issuer: authOptions.Value.ISSUER,
                        audience: authOptions.Value.AUDIENCE,
                        notBefore: DateTime.Now,
                        claims: identity.Claims,
                        expires: DateTime.Now.Add(TimeSpan.FromMinutes(authOptions.Value.LIFETIME)),
                        signingCredentials: new SigningCredentials(signingKey.GetKey(), signingKey.SigningAlgorithm));

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                HttpContext.Response.Cookies.Append(".Core.Geo.Bear", encodedJwt,
                    new CookieOptions
                    {
                        MaxAge = TimeSpan.FromMinutes(authOptions.Value.LIFETIME)
                    });

                return Ok();
            }
            catch (BusinessLogicException bblEx)
            {
                return Ok(JsonResultResponse.Bad(bblEx.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorText = ex.Message });
            }
        }

        [HttpPost("registration")]
        public IActionResult Registration(RegistrationModel model)
        {
            try
            {
                UserActions.TryRegisterUser(_ctx, model);
                return Ok();
            }
            catch(BusinessLogicException bblEx)
            {
                return Ok(JsonResultResponse.Bad(bblEx.Message));
            }
            catch(Exception ex)
            {
                return BadRequest(new { errorText = ex.Message });
            }
        }

        private ClaimsIdentity GetIdentity(LoginModel model) =>
            UserActions.AuthenticationUser(_ctx, model) is User user
            ? new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                },
                "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType)
            : null;
    }
}