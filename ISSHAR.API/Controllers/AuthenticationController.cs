using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ISSHAR.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService , IConfiguration config)
        {
            _userService = userService;
            _configuration = config;
        }

        [HttpPost("register")]
        public async Task<ActionResult> register([FromForm] UserDTO userDto)
        {
            await _userService.AddUserAsync(userDto);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  new Claim(ClaimTypes.Name, userDto.Email),
                  new Claim("FirstName", userDto.FirstName),
                  new Claim("FatherName", userDto.FatherName),
                  new Claim("GrandFatherName", userDto.GrandFatherName), 
                  new Claim("FamilyName", userDto.FamilyName), 
                  new Claim("Email", userDto.Email),
                  new Claim("DateOfBirth", userDto.DateOfBirth.ToString("yyyy-MM-dd")), 
                  new Claim("City", userDto.City),
                  new Claim("Gender", userDto.Gender), 
                  new Claim(ClaimTypes.Role, userDto.Role),
                }),
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString, Message = "User registered successfully." });
        }
        
        [HttpPost("login")]
        public async Task<ActionResult> LogIn(LoginBodyRequest loginBody)
        {
            bool isValidPassword = await _userService.CheckPasswordAsync(loginBody);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid email or password.");
            }

            UserDTO userDto = await _userService.GetUserByEmailAsync(loginBody.Email);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                  new Claim(ClaimTypes.Name, userDto.Email),
                  new Claim(ClaimTypes.Name, userDto.Email),
                  new Claim("FirstName", userDto.FirstName),
                  new Claim("FatherName", userDto.FatherName),
                  new Claim("GrandFatherName", userDto.GrandFatherName),
                  new Claim("FamilyName", userDto.FamilyName),
                  new Claim("Email", userDto.Email),
                  new Claim("DateOfBirth", userDto.DateOfBirth.ToString("yyyy-MM-dd")),
                  new Claim("City", userDto.City),
                  new Claim("Gender", userDto.Gender),
                  new Claim(ClaimTypes.Role, userDto.Role),
                }),
                Expires = DateTime.UtcNow.AddHours(1), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

    }
}
