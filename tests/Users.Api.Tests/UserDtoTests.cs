using Users.Shared;
using Xunit;

namespace Users.Api.Tests
{
    public class UserDtoTests
    {
        [Fact]
        // The 3 parameter constructor is required by Dapper
        public void Pass_Check_UserDto_Has_A_3_Parameter_Constructor()
        {
            UserDto user = new UserDto("","","");
        }
        
        [Fact]
        public void Pass_Check_UserDto_Has_A_4_Parameter_Constructor()
        {
            UserDto user = new UserDto("","","", "");
        }
    }
}