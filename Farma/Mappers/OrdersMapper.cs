using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class OrdersMapper : Profile
    {
        public OrdersMapper()
        {
            CreateMap<OrdersEntity, OrdersDTO>();
            CreateMap<OrdersCreateDTO, OrdersEntity>();
            CreateMap<OrdersUpdateDTO, OrdersEntity>();
            CreateMap<OrdersEntity, OrdersEntity>();
        }
    }
}
