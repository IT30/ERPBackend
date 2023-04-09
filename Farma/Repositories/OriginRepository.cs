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
            OriginEntity origin = mapper.Map<OriginEntity>(originCreateDTO);
            origin.IDOrigin = Guid.NewGuid();
            context.Add(origin);
            return mapper.Map<OriginDTO>(origin);
        }

        public void DeleteOrigin(Guid OriginID)
        {
            OriginEntity? origin = GetOriginByID(OriginID);
            if (origin != null)
                context.Remove(origin);
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
            return context.SaveChanges() > 0;
        }
    }
}
