using GeoService.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GeoService.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseApiController : ControllerBase
    {
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
    }
}