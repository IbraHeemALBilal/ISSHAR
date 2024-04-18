﻿using AutoMapper;
using ISSHAR.Application.DTOs.UserDTOs;
using ISSHAR.DAL.Entities;
using ISSHAR.DAL.Repositories;

namespace ISSHAR.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper; 
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task AddUserAsync(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            await _userRepository.AddAsync(user);
        }
    }
}
