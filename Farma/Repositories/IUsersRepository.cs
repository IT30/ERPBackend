using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public interface IUsersRepository
    {
        List<UsersEntity> GetUsers();

        UsersEntity? GetUserByID(Guid UserID);

        UserDTO CreateUser(UserCreateDTO userCreateDTO);

        void DeleteUser(Guid UserID);

        bool SaveChanges();
    }
}
