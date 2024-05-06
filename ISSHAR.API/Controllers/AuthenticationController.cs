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
        private readonly IJwtGenerator _jwtGenerator;

        public AuthenticationController(IUserService userService, IJwtGenerator jwtGenerator)
        {
            _userService = userService;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("register")]
        public async Task<ActionResult> register([FromForm] UserDTO userDto)
        {
            var existingUser = await _userService.GetUserByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return Conflict("User with the same email already exists.");
            }
            await _userService.AddUserAsync(userDto);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginBodyRequest loginBody)
        {
            bool isValidPassword = await _userService.CheckPasswordAsync(loginBody);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid email or password.");
            }
            var userDto = await _userService.GetUserByEmailAsync(loginBody.Email);
            var tokenString = _jwtGenerator.GenerateJwtToken(userDto);

            return Ok(new
            {
                UserId = userDto.UserId,
                Role = userDto.Role,
                Token = tokenString
            });
        }
    }
}
