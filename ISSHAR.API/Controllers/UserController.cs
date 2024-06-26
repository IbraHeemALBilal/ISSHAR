﻿using ISSHAR.Application.DTOs.HallDTOs;
using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.Application.Services;
using ISSHAR.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ISSHAR.API.Controllers
{
    [Route("ishhar/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserInfoDTO>>> GetAllUsers(int page = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(page, pageSize);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDisplayDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [Authorize(Roles = "Reguler, HallOwner")]
        [HttpGet("receivers/{cartId}")]
        public async Task<ActionResult<ICollection<UserInfoDTO>>> GetReceiversOfCartAsync(int cartId)
        {
            var receivers = await _userService.GetReceiversOfCartAsync(cartId);
            return Ok(receivers);
        }
        [Authorize]
        [HttpGet("filtered")]
        public async Task<ActionResult<ICollection<UserInfoDTO>>> GetFilteredAsync([FromQuery] UserFilterBody userFilterBody)
        {
            var users = await _userService.GetFilteredHallsAsync(userFilterBody);
            return Ok(users);
        }
    }
}
