using AutoMapper;
using Farma.DTO;
using Farma.Entities;

namespace Farma.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly FarmaContext context;
        private readonly IMapper mapper;

        public UsersRepository(FarmaContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public UserDTO CreateUser(UserCreateDTO userCreateDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid UserID)
        {
            UsersEntity? user = GetUserByID(UserID);
            if (user != null)
                context.Remove(user);
        }

        public UsersEntity? GetUserByID(Guid UserID)
        {
            return context.Users.FirstOrDefault(e => e.IDUser == UserID);
        }

        public List<UsersEntity> GetUsers()
        {
            return context.Users.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
