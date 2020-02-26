using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace HolidayOptimizer.Api.Filters
{
    public class ExceptionsFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionsFilter> _logger;

        public ExceptionsFilter(ILogger<ExceptionsFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An error occurred");

            context.Result = GetErrorResult("999", "An error occurred during the operation.");
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.ExceptionHandled = true;
        }

        private static JsonResult GetErrorResult(string code, string message)
        {
            return new JsonResult(
                new
                {
                    Errors = new[]
                    {
                        new { code, message }
                    }
                });
        }
    }
}
