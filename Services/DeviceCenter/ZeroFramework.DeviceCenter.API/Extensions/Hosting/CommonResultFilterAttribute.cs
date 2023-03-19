using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZeroFramework.DeviceCenter.Domain.Exceptions;

namespace ZeroFramework.DeviceCenter.API.Extensions.Hosting
{
    public class CommonResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult objRst)
            {
                if (objRst.Value is ApiResult) return;
                context.Result = new ObjectResult(new ApiResult
                {
                    Success = true,
                    Message = string.Empty,
                    Data = objRst.Value
                });
            }
        }
    }
}
