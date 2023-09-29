using Microsoft.AspNetCore.Mvc;
using NetPcContactApi.Models.User;
using NetPcContactApi.Services;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NetPcContactApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public readonly IConfiguration _configuration;

        public UsersController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }


        /// <summary>
        /// Delete selcted user
        /// </summary>
        [HttpDelete("DeleteUser")]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<ServiceResponse<string>>> Users(DeleteUserDto deleteUserDto)
        {
            var response = await _userService.DeleteUser(deleteUserDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Get users collection
        /// </summary>
        /// <returns>Colletion of users data without password and empty tokens</returns>
        [HttpGet("Users")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> Users()
        {
            var response = await _userService.GetUsers();
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="userDto">Data of the user to be registered</param>
        /// <returns>User data if registration was successful</returns>
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> Register(UserDto userDto)
        {
            var response = await _userService.Register(userDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Login an existing user
        /// </summary>
        /// <param name="userDto">User login data</param>
        /// <returns>User data if login was successful</returns>
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> Login(UserLoginDto userDto)
        {
            var response = await _userService.Authenticate(userDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="userDto">New user data to be updated</param>
        /// <returns>Updated user data if operation was successful</returns>
        [Authorize(Roles = "user, admin")]
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ServiceResponse<UserResponse>>> UpdateUser(UpdateUserDto userDto)
        {
            var response = await _userService.UpdateUser(GetUserToken(), userDto);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        /// <summary>
        /// Extracts user's token from the Authorization header
        /// </summary>
        /// <returns>User's token</returns>
        private string GetUserToken()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            return authorizationHeader.Substring("Bearer ".Length);
        }
    }
}
