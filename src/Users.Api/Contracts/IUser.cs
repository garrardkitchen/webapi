using Users.Shared;

namespace Users.Api.Contracts
{
    public interface IUser
    {
        UserDto GetUser(string email);
        UserDto AddUser(UserDto user);
        UserDto UpdateUser(UserDto user);
    }
}