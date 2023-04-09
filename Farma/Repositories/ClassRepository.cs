using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public ClassRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public ClassDTO CreateClass(ClassCreateDTO classCreateDTO)
        {
            ClassEntity classs = mapper.Map<ClassEntity>(classCreateDTO);
            classs.IDClass = Guid.NewGuid();
            context.Add(classs);
            return mapper.Map<ClassDTO>(classs);
        }

        public void DeleteClass(Guid ClassID)
        {
            ClassEntity? classe = GetClassByID(ClassID);
            if (classe != null)
                context.Remove(classe);
        }

        public ClassEntity? GetClassByID(Guid ClassID)
        {
            return context.Class.FirstOrDefault(e => e.IDClass == ClassID);
        }

        public List<ClassEntity> GetClasses()
        {
            return context.Class.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
