using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HolidayOptimizer.Api.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            return base.Ok(new { data = value });
        }
    }
}
