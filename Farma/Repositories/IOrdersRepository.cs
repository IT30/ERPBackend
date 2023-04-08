using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IOrdersRepository
    {
        List<OrdersEntity> GetOrders();

        OrdersEntity? GetOrderByID(Guid OrderID);

        OrdersDTO CreateOrder(OrdersCreateDTO ordersCreateDTO);

        void DeleteOrder(Guid OrderID);

        bool SaveChanges();
    }
}
