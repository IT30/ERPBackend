using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IOrderItemRepository
    {
        List<OrderItemEntity> GetOrderItems();

        OrderItemEntity? GetOrderItemByID(Guid OrderItemID);

        List<OrderItemEntity> GetOrderItemsByOrder(Guid IDUser);

        OrderItemDTO CreateOrderItem(OrderItemCreateDTO orderItemCreateDTO);

        void DeleteOrderItem(Guid OrderItemID);

        bool SaveChanges();
    }
}
