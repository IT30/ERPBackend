using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<ProductEntity, ProductDTO>();
            CreateMap<ProductCreateDTO, ProductEntity>();
            CreateMap<ProductUpdateDTO, ProductEntity>();
            CreateMap<ProductEntity, ProductEntity>();
        }
    }
}
