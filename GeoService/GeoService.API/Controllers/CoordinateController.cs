using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.Models;
using GeoService.DAL;
using GeoService.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoordinateController : ControllerBase
    {

        private readonly GeoContext _ctx;
        public CoordinateController(GeoContext context)
        {
            _ctx = context;
        }

        [HttpPost]
        public ActionResult AddManualPoint(ManualPointModel model)
        {
            try
            {
                _ctx.Coordinates.Add(new Coordinate 
                {
                    Altitude = model.Altitude,
                    Longitude = model.Longitude,
                    Latitude = model.Latitude,
                    CreateTime = DateTime.Now,
                    Mode = ModeEnum.Manual,
                });
                _ctx.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }




    }
}