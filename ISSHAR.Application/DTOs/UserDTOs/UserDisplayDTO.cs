﻿using ISSHAR.DAL.Enums;

namespace ISSHAR.Application.DTOs.UserDTOs
{
    public class UserDisplayDTO
    {
        public int UserId { set; get; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string FamilyName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { set; get; }
        public DateTime DateOfBirth { get; set; }
        public string City { set; get; }
        public string Gender { set; get; }
        public Role Role { get; set; }

    }
}
