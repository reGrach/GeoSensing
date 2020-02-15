using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoService.API.BusinessLogic;
using GeoService.API.BusinessLogic.Utils;
using GeoService.API.Models;
using GeoService.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeoService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly GeoContext _ctx;
        public TeamController(GeoContext context)
        {
            _ctx = context;
        }

        [HttpPost]
        public IActionResult CreateTeam(CreateTeamModel model)
        {
            try
            {
                // TODO: получить Id создателя команды
                TeamActions.CreateTeam(_ctx, model, 0);
                return Ok();
            }
            catch (BusinessLogicException bblEx)
            {
                return JsonResultResponse.Bad(bblEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest((new { errorText = ex.Message }));
            }
        }

        [HttpPost]
        public IActionResult GetTeam(int idTeam)
        {
            try
            {
                var dbTeam = TeamActions.GetTeamById(_ctx, idTeam);
                return JsonResultResponse.Ok(dbTeam);
            }
            catch (BusinessLogicException bblEx)
            {
                return JsonResultResponse.Bad(bblEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest((new { errorText = ex.Message }));
            }
        }

        [HttpGet]
        public IActionResult GetAllTeams()
        {
            try
            {
                var dbTeams = TeamActions.GetAllTeam(_ctx);
                return JsonResultResponse.Ok(dbTeams);
            }
            catch (BusinessLogicException bblEx)
            {
                return JsonResultResponse.Bad(bblEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest((new { errorText = ex.Message }));
            }
        }

        [HttpPost]
        public IActionResult AddPolygon(PolygonTeamModel model)
        {
            try
            {
                if (model.PointsX.Length != model.PointsY.Length)
                    throw new BusinessLogicException("Полигон задан неверно");

                var dbTeam = TeamActions.GetTeamById(_ctx, model.Id);
                dbTeam.Polygon = ConvertToPostgreType.ToPolygon(model.PointsX, model.PointsY);
                _ctx.SaveChanges();
                return Ok();
            }
            catch (BusinessLogicException bblEx)
            {
                return JsonResultResponse.Bad(bblEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest((new { errorText = ex.Message }));
            }
        }

        [HttpDelete]
        public IActionResult DeletePolygon(int idTeam)
        {
            try
            {
                var dbTeam = TeamActions.GetTeamById(_ctx, idTeam);
                dbTeam.Polygon = null;
                _ctx.SaveChanges();
                return Ok();
            }
            catch (BusinessLogicException bblEx)
            {
                return JsonResultResponse.Bad(bblEx.Message);
            }
            catch (Exception ex)
            {
                return BadRequest((new { errorText = ex.Message }));
            }
        }
    }
}