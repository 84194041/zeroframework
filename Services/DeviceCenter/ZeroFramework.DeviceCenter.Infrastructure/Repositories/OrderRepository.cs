using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate;
using ZeroFramework.DeviceCenter.Infrastructure.EntityFrameworks;

namespace ZeroFramework.DeviceCenter.Infrastructure.Repositories
{
    public class OrderRepository : EfCoreRepository<DeviceCenterDbContext, Order>, IOrderRepository
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderRepository(DeviceCenterDbContext dbContext, IOrderItemRepository orderItemRepository) : base(dbContext)
        {
            _orderItemRepository = orderItemRepository;
        }

        public Order Add(Order order)
        {
            return DbSet.Add(order).Entity;
        }

        public async Task<Order> GetAsync(Guid orderId)
        {
            var order = await DbSet.Include(x => x.Address).FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
            {
                order = DbSet.Local.FirstOrDefault(o => o.Id == orderId);
            }

            if (order is not null)
            {
                await _dbContext.Entry(order).Collection(i => i.OrderItems).LoadAsync();
                await _dbContext.Entry(order).Reference(i => i.OrderStatus).LoadAsync();
            }

            return order ?? throw new NullReferenceException();
        }

        public void Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
        }

        public async Task<List<Order>> GetOrderListAsync()
        {
            var query = (await _dbContext.Set<Order>()
            .OrderBy(t => t.Id)
            .Skip((1 - 1) * 1).Take(1)
            .GroupJoin(_dbContext.Set<OrderItem>(), left => left.Id, right => right.OrderId, (left, right) => new
            {
                Id = left.Id,
                BuyerId = left.BuyerId,
                OrderItem = right
            }).SelectMany(left => left.OrderItem.DefaultIfEmpty(), (left, right) => new OrderItem1
            {
                OrderId = left.Id,
                ProductId = right!.ProductId
            })
            .ToListAsync())
            .GroupBy(d => d.OrderId)
            .Select(g =>
            {
                var first = g.FirstOrDefault();
                return new OrderListResponseModel1
                {
                    OrderId = first!.OrderId,
                    OrderItems = g.GroupBy(d => new { d.OrderId, d.ProductId })
                                  .Select(i => new OrderItem1 { OrderId = i.Key.OrderId, ProductId = i.Key.ProductId }).ToList()
                };
            }).ToList();

            return null!;
        }
    }

    public class OrderListResponseModel1
    {
        public Guid OrderId { get; set; }

        public List<OrderItem1> OrderItems = new List<OrderItem1>();
    }

    public class OrderItem1
    {
        public Guid OrderId { get; set; }

        public int ProductId { get; set; }

        public decimal UnitPrice { get; set; }

        public int Units { get; set; }
    }
}