using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IClassRepository
    {
        List<ClassEntity> GetClasses();

        ClassEntity? GetClassByID(Guid ClassID);

        ClassDTO CreateClass(ClassCreateDTO classCreateDTO);

        void DeleteClass(Guid ClassID);

        bool SaveChanges();
    }
}
