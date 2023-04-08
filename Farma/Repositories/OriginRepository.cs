using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class OriginRepository : IOriginRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public OriginRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public OriginDTO CreateOrigin(OriginCreateDTO originCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrigin(Guid OriginID)
        {
            throw new NotImplementedException();
        }

        public OriginEntity? GetOriginByID(Guid OriginID)
        {
            return context.Origin.FirstOrDefault(e => e.IDOrigin == OriginID);
        }

        public List<OriginEntity> GetOrigins()
        {
            return context.Origin.ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
