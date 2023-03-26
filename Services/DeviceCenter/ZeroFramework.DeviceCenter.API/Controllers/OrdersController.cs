using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZeroFramework.DeviceCenter.Application.IntegrationEvents.Events.Ordering;
using ZeroFramework.DeviceCenter.Application.Models.Ordering;
using ZeroFramework.DeviceCenter.Application.Queries.Ordering;
using ZeroFramework.DeviceCenter.Application.Services.Ordering;
using ZeroFramework.EventBus.Abstractions;
using ZeroFramework.Payment.WeChat.Models;
using ZeroFramework.Payment.WeChat.Services;

namespace ZeroFramework.DeviceCenter.API.Controllers
{
    //[ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly ILogger<OrdersController> _logger;

        private readonly IOrderQueries _orderQueries;

        private readonly IMediator _mediator;

        private readonly IOrderApplicationService _orderApplicationService;

        private readonly IEventBus _eventBus;

        private readonly IServiceProvider _serviceProvider;

        private readonly IHttpClientFactory _httpClientFactory;

        public OrdersController(IServiceProvider serviceProvider, ILogger<OrdersController> logger, IOrderQueries orderQueries, IMediator mediator, IOrderApplicationService orderApplicationService, IEventBus eventBus, IHttpClientFactory httpClientFactory)
        {
            _orderQueries = orderQueries;
            _mediator = mediator;
            _orderApplicationService = orderApplicationService;
            _eventBus = eventBus;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _httpClientFactory = httpClientFactory;
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
            try
            {
                WeChatPayConfig weChatPayConfig = new WeChatPayConfig("", "", "", "", "", "", "", "", "");
                WeChatPayService weChatPayService = new WeChatPayService(_logger, weChatPayConfig, _httpClientFactory);

                weChatPayService.Pay("1", "1", "1");

                await _eventBus.PublishAsync(new OrderPaymentFailedIntegrationEvent(Guid.NewGuid()) { Id = Guid.NewGuid(), CreationTime = DateTime.Now });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Ok();
            //var list = await _orderApplicationService.GetOrderListAsync(model);
            //return Ok(list);
            //return Content("this is content");
            //throw new BizException("返回错误信息");
        }
    }
}