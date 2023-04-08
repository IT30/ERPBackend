using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Mappers
{
    public class ClassMapper : Profile
    {
        public ClassMapper()
        {
            CreateMap<ClassEntity, ClassDTO>();
            CreateMap<ClassCreateDTO, ClassEntity>();
            CreateMap<ClassUpdateDTO, ClassEntity>();
            CreateMap<ClassEntity, ClassEntity>();
        }
    }
}
