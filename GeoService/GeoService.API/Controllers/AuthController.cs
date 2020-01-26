using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GeoService.API.Auth;
using GeoService.API.Models;
using GeoService.BLL;
using GeoService.BLL.DTO;
using GeoService.BLL.Enums;
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
        public AuthController(IOptions<AuthOptions> config)
        {
            authOptions = config;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(JsonResultResponse.Ok(authOptions));
        }

        [HttpPost("login")]
        public ActionResult<string> Login(LoginModel model, [FromServices] IJwtSigningEncodingKey signingKey)
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

            return Ok(JsonResultResponse.Ok(encodedJwt));
        }

        [HttpPost("registration")]
        public ActionResult<string> Registration(LoginModel model)
        {
            try
            {
                UserAction.TryRegisterUser(model.Login, model.Password);
                return Ok();
            }
            catch(BusinessLogicException bblEx)
            {
                return Ok(JsonResultResponse.Ok(bblEx.Message));
            }
            catch(Exception ex)
            {
                return BadRequest(new { errorText = ex.Message });
            }
        }

        private ClaimsIdentity GetIdentity(LoginModel model) =>
            UserAction.LoginUser(model.Login, model.Password) is UserDTO user
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