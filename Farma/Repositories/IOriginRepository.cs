using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IOriginRepository
    {
        List<OriginEntity> GetOrigins();

        OriginEntity? GetOriginByID(Guid OriginID);

        OriginDTO CreateOrigin(OriginCreateDTO originCreateDTO);

        void DeleteOrigin(Guid OriginID);

        bool SaveChanges();
    }
}
