using MediatR;
using Microsoft.AspNetCore.Mvc;
using ZeroFramework.DeviceCenter.Application.Models.Ordering;
using ZeroFramework.DeviceCenter.Application.Queries.Ordering;
using ZeroFramework.DeviceCenter.Application.Services.Ordering;

namespace ZeroFramework.DeviceCenter.API.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrderQueries _orderQueries;

        private readonly IMediator _mediator;

        private readonly IOrderApplicationService _orderApplicationService;

        public OrdersController(IOrderQueries orderQueries, IMediator mediator, IOrderApplicationService orderApplicationService)
        {
            _orderQueries = orderQueries;
            _mediator = mediator;
            _orderApplicationService = orderApplicationService;
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(OrderViewModel), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //public async Task<ActionResult> GetOrderAsync(Guid orderId)
        //{
        //    //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
        //    //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
        //    OrderViewModel orderViewModel = await _orderQueries.GetOrderAsync(orderId);
        //    return Ok(orderViewModel);
        //}

        //[HttpPut]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<IActionResult> CancelOrderAsync([FromBody] CancelOrderCommand command, [FromHeader(Name = "X-Request-Id")] string? requestId)
        //{
        //    requestId ??= Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        //    var identifiedCommand = new IdentifiedCommand<CancelOrderCommand, bool>(command, requestId);
        //    await _mediator.Send(identifiedCommand);

        //    return Ok();
        //}

        [HttpGet]
        public async Task<ActionResult> GetOrders([FromQuery] OrderListRequestModel model)
        {
            var list = await _orderApplicationService.GetOrderListAsync(model);
            return Ok(list);
            //return Content("this is content");
            //throw new BizException("返回错误信息");
        }
    }
}