using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Users.Api;
using Users.Api.Infrastructure;
using Users.Shared;

namespace Users.Web.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET api/user/garrard.kitchen@gmail
        [HttpGet("{email}")]
        public ActionResult<string> Get(string email)
        {
            IRepository<UserDto> repository = new Repository(Config.ConnectionString);
            Users.Api.User userApi = new User(repository);
            UserDto user = userApi.GetUser(email);
            if (user == null)
            {
                return BadRequest(new {message = "This email does not exist"}); 
            } else {
                return Ok(new{firstname=user.Firstname, surname=user.Surname, email=user.Email});
            }
        }

        // PUT api/user/update
        [HttpPut ("update")]
        public ActionResult<string> Put([FromBody] UserDto userDto)
        {
            IRepository<UserDto> repository = new Repository(Config.ConnectionString);
            Users.Api.User userApi = new User(repository);
            UserDto user = userApi.UpdateUser(userDto);
            return Ok(new{firstname=user.Firstname, surname=user.Surname, email=user.Email});
        }
        
        [HttpPost ("add")]
        public ActionResult<string> Post([FromBody] UserDto userDto)
        {
            IRepository<UserDto> repository = new Repository(Config.ConnectionString);
            Users.Api.User userApi = new User(repository);

            try
            {
                UserDto user = userApi.AddUser(userDto);
                return Ok(new {firstname = user.Firstname, surname = user.Surname, email = user.Email});
            }
            catch (DuplicateUserException)
            {
                return BadRequest(new {message = "This email already exists"});
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
