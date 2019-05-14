using Xunit;

namespace Users.Api.Tests
{
    public class SecurityTests
    {
        [Fact]
        public void Pass_Check_Password_Hash_Test()
        {
            var password = "ABC";
            var hashed = "3C01BDBB26F358BAB27F267924AA2C9A03FCFDB8";
            
            Assert.True(Users.Api.Infrastructure.Security.GetSHA1Hash(password) == hashed);
        }
        
    }
}