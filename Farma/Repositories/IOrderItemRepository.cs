using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IOrderItemRepository
    {
        List<OrderItemEntity> GetOrderItems();

        OrderItemEntity? GetOrderItemByID(Guid OrderItemID);

        OrderItemDTO CreateOrderItem(OrderItemCreateDTO orderItemCreateDTO);

        void DeleteOrderItem(Guid OrderItemID);

        bool SaveChanges();
    }
}
