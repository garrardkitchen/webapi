using Users.Shared;

namespace Users.Api
{
    public interface IUser
    {
        UserDto GetUser(string email);
        UserDto AddUser(UserDto user);
        UserDto UpdateUser(UserDto user);
    }
}