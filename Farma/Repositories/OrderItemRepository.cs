using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public OrderItemRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public OrderItemDTO CreateOrderItem(OrdersCreateDTO orderItemCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrderItem(Guid OrderItemID)
        {
            OrderItemEntity? orderItem = GetOrderItemByID(OrderItemID);
            if (orderItem != null)
                context.Remove(orderItem);
        }

        public OrderItemEntity? GetOrderItemByID(Guid OrderItemID)
        {
            return context.OrderItems.FirstOrDefault(e => e.IDOrderItem == OrderItemID);
        }

        public List<OrderItemEntity> GetOrderItems()
        {
            return context.OrderItems.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
