using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> SignUp([FromForm] UserDTO userDto)
        {
            await _userService.AddUserAsync(userDto);
            return Ok("User registered successfully.");
        }
        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginBodyRequest loginBody)
        {
            bool isValidPassword =await _userService.CheckPasswordAsync(loginBody);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid email or password.");
            }
            return Ok("Login successful.");
        }

    }
}
