using AutoMapper;
using Farma.DTO;
using Farma.Entities;
using System.Security.Cryptography;

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
            Tuple<string, string> hashPassword = HashPassword(userCreateDTO.PwdHash);
            UsersEntity user = mapper.Map<UsersEntity>(userCreateDTO);
            user.IDUser = Guid.NewGuid();
            user.PwdHash = hashPassword.Item1;
            user.PwdSalt = hashPassword.Item2;
            user.UserRole = "USER";
            context.Add(user);
            return mapper.Map<UserDTO>(user);
        }

        public UserDTO UpdateUser(UserUpdateDTO userUpdateDTO)
        {
            UsersEntity? oldUser = context.Users.FirstOrDefault(e => e.IDUser == Guid.Parse(userUpdateDTO.IDUser));
            UsersEntity user = mapper.Map<UsersEntity>(userUpdateDTO);
            Tuple<string, string> hashPassword = HashPassword(userUpdateDTO.PwdHash);
            user.PwdHash = hashPassword.Item1;
            user.PwdSalt = hashPassword.Item2;
            mapper.Map(user, oldUser);
            return mapper.Map<UserDTO>(user);
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

        private static Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];
            RandomNumberGenerator.Create().GetNonZeroBytes(sBytes);
            var salt = Convert.ToBase64String(sBytes);

            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, 1000);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }
    }
}
