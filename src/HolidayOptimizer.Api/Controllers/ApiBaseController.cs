using HolidayOptimizer.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace HolidayOptimizer.Api.Controllers
{
    public class ApiBaseController : ControllerBase
    {        
        public OkObjectResult Ok<TData>(TData value)
        {
            return base.Ok(new ApiResponse<TData> { Data = value });
        }
    }
}
