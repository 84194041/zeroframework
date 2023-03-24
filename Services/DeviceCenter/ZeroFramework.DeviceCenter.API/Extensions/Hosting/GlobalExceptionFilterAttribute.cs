using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZeroFramework.DeviceCenter.Domain.Exceptions;

namespace ZeroFramework.DeviceCenter.API.Extensions.Hosting
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilterAttribute> _logger;

        public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var isBizExp = context.Exception is BizException;
            if (isBizExp)
            {
                context.Result = new ObjectResult(new ApiResult
                {
                    Success = false,
                    Message = context.Exception.Message
                });
            }
            //非业务异常记录errorLog,返回500状态码，前端通过捕获500状态码进行友好提示
            //if (isBizExp == false)
            //{
            //    _logger.LogError(context.Exception, context.Exception.Message);
            //    context.HttpContext.Response.StatusCode = 500;
            //}
            base.OnException(context);
        }
    }
}
