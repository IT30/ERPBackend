using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class OrderItemMapper : Profile
    {
        public OrderItemMapper()
        {
            CreateMap<OrderItemEntity, OrderItemDTO>();
            CreateMap<OrderItemCreateDTO, OrderItemEntity>();
            CreateMap<OrderItemUpdateDTO, OrderItemEntity>();
            CreateMap<OrderItemEntity, OrderItemEntity>();
        }
    }
}
