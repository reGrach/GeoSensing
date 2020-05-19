using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Auth;
using GeoService.API.Auth.Identity;
using GeoService.API.Models;
using static GeoService.BLL.Actions.UserActions;
using GeoService.BLL.DTO;
using GeoService.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GeoService.API.Auth.Identity.Contracts;


namespace GeoService.API.Controllers
{
    public class ProfileController : BaseApiController
    {
        private readonly GeoContext _context;

        public ProfileController(GeoContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get() => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var profile = _context.GetProfile(id);
            return Ok(profile);
        });


        [HttpPost]
        public IActionResult Update(ProfileModel model) => TryAction(() =>
        {
            var id = User.Identity.GetUserId();
            var user = new UserDTO
            {
                Name = model.Name,
                SurName = model.SurName,
                Login = model.Login
            };
            _context.UpdateProfile(id, user);
            return Ok();
        });
    }
}