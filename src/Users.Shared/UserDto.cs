using System;

namespace Users.Shared
{
    public class UserDto
    {

        public UserDto()
        {
            
        }
        public UserDto(string firstname, string surname, string email, string password)
        {
            Firstname = firstname;
            Surname = surname;
            Email = email;
            Password = password;
        }
        
        public UserDto(string firstname, string surname, string email)
        {
            Firstname = firstname;
            Surname = surname;
            Email = email;
        }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        
    }
}
