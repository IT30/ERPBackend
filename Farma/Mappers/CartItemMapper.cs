using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class CartItemMapper : Profile
    {
        public CartItemMapper()
        {
            CreateMap<CartItemEntity, CartItemDTO>();
            CreateMap<CartItemCreateDTO, CartItemEntity>();
            CreateMap<CartItemUpdateDTO, CartItemEntity>();
            CreateMap<CartItemEntity, CartItemEntity>();
        }
    }
}
