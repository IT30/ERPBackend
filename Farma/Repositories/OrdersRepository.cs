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
            throw new NotImplementedException();
        }

        public void DeleteOrder(Guid OrderID)
        {
            throw new NotImplementedException();
        }

        public OrdersEntity? GetOrderByID(Guid OrderID)
        {
            throw new NotImplementedException();
        }

        public List<OrdersEntity> GetOrders()
        {
            return context.Orders.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
