using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogAPI.Models;
using blogAPI.Models.DTO;
using blogAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        //we will need to dependency inject our services!
        private readonly UserService _userData;
        public UserController(UserService dataFromService)
        {
            _userData = dataFromService;
        }

        //Function to add our user type of CreateAccountDTO call UserToAdd this will return bool once our user is added!
        [HttpPost("AddUser")]
        public bool UserToAdd(CreateAccountDTO userToAdd)
        {
            return _userData.AddUser(userToAdd);
        }

        //getall Users, getUserById.
        [HttpGet("GetAllUsers")]
        public IEnumerable<UserModel> GetAllUsers()
        {
            return _userData.GetAllUsers();
        }

        [HttpGet("GetUserByUsername")]
        public UserIdDTO GetUserIdDTOUsername(string username)
        {
            return _userData.GetUserIdDTOByUsername(username);
        }

        //Login Method endpoint
        [HttpPost("Login")]
        // IACtionREsult returns methods like okay! 
        public IActionResult Login([FromBody] LoginDTO user)
        {
            return _userData.Login(user);
        }

    }
}