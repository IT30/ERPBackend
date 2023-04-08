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
            throw new NotImplementedException();
        }

        public void DeleteClass(Guid ClassID)
        {
            throw new NotImplementedException();
        }

        public ClassEntity? GetClassByID(Guid ClassID)
        {
            throw new NotImplementedException();
        }

        public List<ClassEntity> GetClasses()
        {
            return context.Class.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
