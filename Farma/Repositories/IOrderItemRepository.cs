using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IOrderItemRepository
    {
        List<OrderItemEntity> GetOrderItems();

        OrderItemEntity? GetOrderItemByID(Guid OrderItemID);

        OrderItemDTO CreateOrderItem(OrdersCreateDTO orderItemCreateDTO);

        void DeleteOrderItem(Guid OrderItemID);

        bool SaveChanges();
    }
}
