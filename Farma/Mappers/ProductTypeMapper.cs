using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class ProductTypeMapper : Profile
    {
        public ProductTypeMapper()
        {
            CreateMap<ProductTypeEntity, ProductTypeDTO>();
            CreateMap<ProductTypeCreateDTO, ProductTypeEntity>();
            CreateMap<ProductTypeUpdateDTO, ProductTypeEntity>();
            CreateMap<ProductTypeEntity, ProductTypeEntity>();
        }
    }
}
