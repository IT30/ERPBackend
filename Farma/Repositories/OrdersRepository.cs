using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public OrdersRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public OrdersDTO CreateOrder(OrdersCreateDTO ordersCreateDTO)
        {
            OrdersEntity orders = mapper.Map<OrdersEntity>(ordersCreateDTO);
            orders.IDOrder = Guid.NewGuid();
            orders.TransactionDate = DateTime.Now;
            context.Add(orders);
            return mapper.Map<OrdersDTO>(orders);
        }

        public void DeleteOrder(Guid OrderID)
        {
            OrdersEntity? order = GetOrderByID(OrderID);
            if (order != null)
                context.Remove(order);
        }

        public OrdersEntity? GetOrderByID(Guid OrderID)
        {
            return context.Orders.FirstOrDefault(e => e.IDOrder == OrderID);
        }

        public List<OrdersEntity> GetOrders()
        {
            return context.Orders.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
