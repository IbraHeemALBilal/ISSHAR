using ISSHAR.API.Interfaces;
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
        private readonly IJwtWhitelistService _jwtWhitelistService;

        public AuthenticationController(IUserService userService, IJwtGenerator jwtGenerator , IJwtWhitelistService jwtWhitelistService)
        {
            _userService = userService;
            _jwtGenerator = jwtGenerator;
            _jwtWhitelistService = jwtWhitelistService;
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
            await _jwtWhitelistService.AddTokenAsync(tokenString);
            return Ok(new
            {
                UserId = userDto.UserId,
                FirstName= userDto.FirstName,
                FamilyName = userDto.FamilyName,
                Role = userDto.Role,
                Token = tokenString
            });
        }
        [HttpPost("logout")]
        public async Task<ActionResult> LogOut()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            await _jwtWhitelistService.RemoveTokenAsync(token);

            return Ok("Logged out successfully.");
        }
    }
}
