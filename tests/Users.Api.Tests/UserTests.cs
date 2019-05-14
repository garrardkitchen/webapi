using System;
using Moq;
using Users.Shared;
using Xunit;

namespace Users.Api.Tests
{
    public class UserTests
    {
        [Fact]
        public void Fail_Get_User_That_Does_Not_Exist_Test()
        {
            var mock = new Mock<IRepository<UserDto>>();
            mock.Setup(x => x.FindUserByEmail(It.IsAny<string>())).Returns((UserDto)null);
            User user = new User(mock.Object);
            UserDto userDto = user.GetUser("garrardpkitchen@yahoo.co.uk");
            
            Assert.True(userDto == null, "User incorrectly exists");
        }
        
        [Fact]
        public void Pass_Get_User_That_Does_Exist_Test()
        {
            var mock = new Mock<IRepository<UserDto>>();
            mock.Setup(x => x.FindUserByEmail(It.IsAny<string>())).Returns(new UserDto("garrard", "kitchen", "garrardpkitchen@yahoo.co.uk",""));
            User user = new User(mock.Object);
            UserDto userDto = user.GetUser("garrardpkitchen@yahoo.co.uk");
            
            Assert.True(userDto != null, "User does not exists");
            Assert.True(userDto.Firstname ==  "garrard", "Incorrect firstname");
            Assert.True(userDto.Surname ==  "kitchen", "Incorrect surname");
            Assert.True(userDto.Email ==  "garrardpkitchen@yahoo.co.uk", "Incorrect email");
        }
        
        [Fact]
        public void Pass_Insert_New_User_Test()
        {
            var mockUser = new UserDto("garrard", "kitchen", "garrardpkitchen@yahoo.co.uk", "");
            var mock = new Mock<IRepository<UserDto>>();
            mock.Setup(x => x.Add(It.IsAny<UserDto>())).Returns(mockUser);
            User user = new User(mock.Object);
            UserDto userDto = user.AddUser(mockUser);
            
            Assert.True(userDto != null, "User does not exists");
            Assert.True(userDto.Firstname ==  "garrard", "Incorrect firstname");
            Assert.True(userDto.Surname ==  "kitchen", "Incorrect surname");
            Assert.True(userDto.Email ==  "garrardpkitchen@yahoo.co.uk", "Incorrect email");
        }
        
        [Fact]
        public void Fail_Insert_Dupllicate_User_Test()
        {
            var mockUser = new UserDto("garrard", "kitchen", "garrardpkitchen@yahoo.co.uk", "");
            var mock = new Mock<IRepository<UserDto>>();
            mock.Setup(x => x.Add(It.IsAny<UserDto>())).Throws<DuplicateUserException>();
            User user = new User(mock.Object);
            
            Assert.Throws<DuplicateUserException>(() => { user.AddUser(mockUser); });
        }
        
        [Fact]
        public void Pass_Update_Existing_User_Test()
        {
            var mockUser = new UserDto("garrard2", "kitchen2", "garrardpkitchen@yahoo.co.uk2", "");
            var mock = new Mock<IRepository<UserDto>>();
            mock.Setup(x => x.Update(It.IsAny<UserDto>())).Returns(mockUser);
            User user = new User(mock.Object);
            UserDto userDto = user.UpdateUser(mockUser);
            
            Assert.True(userDto != null, "User does not exists");
            Assert.True(userDto.Firstname ==  "garrard2", "Incorrect firstname");
            Assert.True(userDto.Surname ==  "kitchen2", "Incorrect surname");
            Assert.True(userDto.Email ==  "garrardpkitchen@yahoo.co.uk2", "Incorrect email");
        }

       
    }
}
