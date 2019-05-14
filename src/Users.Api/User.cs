using Users.Shared;

namespace Users.Api
{
    public class User : IUser
    {
        public IRepository<UserDto> Repository { get; }

        public User(IRepository<UserDto> repository)
        {
            Repository = repository;
        }

        public UserDto GetUser(string email)
        {
            return Repository.FindUserByEmail(email);
        }

        public UserDto AddUser(UserDto user)
        {
            user.Password = Users.Api.Infrastructure.Security.GetSHA1Hash(user.Password);
            return Repository.Add(user);
        }
        
        public UserDto UpdateUser(UserDto user)
        {
            user.Password = Users.Api.Infrastructure.Security.GetSHA1Hash(user.Password);
            return Repository.Update(user);
        }
    }
}
