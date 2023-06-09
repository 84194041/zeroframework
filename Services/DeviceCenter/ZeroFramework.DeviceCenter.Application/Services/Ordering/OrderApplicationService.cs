﻿using AutoMapper;
using ZeroFramework.DeviceCenter.Application.Models.Ordering;
using ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate;
using ZeroFramework.DeviceCenter.Domain.Services.Ordering;

namespace ZeroFramework.DeviceCenter.Application.Services.Ordering
{
    public class OrderApplicationService : IOrderApplicationService
    {
        private readonly IOrderDomainService _orderDomainService;

        //private readonly IRepository<Order> _orderRepository;

        //private readonly IEventBus _eventBus;

        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderRepository;

        private readonly IOrderItemRepository _orderItemRepository;

        public OrderApplicationService(IOrderDomainService orderDomainService,
            //IRepository<Order> orderRepository, 
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            //IEventBus eventBus, 
            IMapper mapper)
        {
            _orderDomainService = orderDomainService;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            //_eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(OrderCreateRequestModel model, CancellationToken cancellationToken = default)
        {
            Order order = _mapper.Map<Order>(model);

            await _orderDomainService.AddAsync(order, cancellationToken);

            //await _eventBus.PublishAsync(new OrderStartedIntegrationEvent(Guid.NewGuid()), cancellationToken);

            return await Task.FromResult(true);
        }

        public async Task<List<OrderListResponseModel>> GetListAsync(OrderListRequestModel model, CancellationToken cancellationToken = default)
        {
            List<Order> orders = await _orderRepository.GetListAsync(model.PageNumber, model.PageSize, sorting: o => o.CreationTime, cancellationToken: cancellationToken);

            return _mapper.Map<List<OrderListResponseModel>>(orders);
        }

        public async Task<List<OrderListResponseModel>> GetOrderListAsync(OrderListRequestModel model, CancellationToken cancellationToken = default)
        {
            var list = await _orderRepository.GetOrderListAsync();

            return null!;
        }
    }
}
