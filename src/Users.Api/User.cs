using Users.Api.Contracts;
using Users.Api.Exceptions;
using Users.Shared;

namespace Users.Api
{
    // TODO: Vaidate dto, ensure each field has a value and return NullUserException with appropriate message if not (G. Kitchen)
    public class User : IUser
    {
        public IRepository<UserDto> Repository { get; }

        public User(IRepository<UserDto> repository)
        {
            Repository = repository;
        }

        public UserDto GetUser(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new NullUserException("Incomplete user details");
            var user = Repository.FindUserByEmail(email); 
            if (user == null) throw new NullUserException("User not found");
            return user;
        }

        public UserDto AddUser(UserDto user)
        {
            if (user == null) throw new NullUserException("Incomplete user details");
            
            user.Password = Users.Api.Infrastructure.Security.GetSHA1Hash(user.Password);
            return Repository.Add(user);
        }
        
        public UserDto UpdateUser(UserDto user)
        {
            if (user == null) throw new NullUserException("Incomplete user details");
            
            user.Password = Users.Api.Infrastructure.Security.GetSHA1Hash(user.Password);
            return Repository.Update(user);
        }
    }
}
