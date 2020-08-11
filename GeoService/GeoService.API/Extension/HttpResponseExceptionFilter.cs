using GeoService.BLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.Extension
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is ApiException exception)
            {
                context.Result = new ObjectResult(exception.Value)
                {
                    StatusCode = exception.StatusCode,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is Exception ex)
            {
                context.Result = new ObjectResult(ex.Message) { StatusCode = 500 };
                context.ExceptionHandled = true;
            }
        }
    }
}
