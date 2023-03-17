using ZeroFramework.DeviceCenter.Domain.Aggregates.OrderAggregate;
using ZeroFramework.DeviceCenter.Infrastructure.EntityFrameworks;

namespace ZeroFramework.DeviceCenter.Infrastructure.Repositories
{
    public class OrderItemRepository : EfCoreRepository<DeviceCenterDbContext, OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(DeviceCenterDbContext dbContext) : base(dbContext) { }
    }
}
