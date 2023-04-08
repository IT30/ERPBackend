using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class OriginMapper : Profile
    {
        public OriginMapper()
        {
            CreateMap<OriginEntity, OriginDTO>();
            CreateMap<OriginCreateDTO, OriginEntity>();
            CreateMap<OriginUpdateDTO, OriginEntity>();
            CreateMap<OriginEntity, OriginEntity>();
        }
    }
}
