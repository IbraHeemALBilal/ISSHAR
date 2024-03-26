﻿namespace ISSHAR.DAL.Entities
{
    public class User
    {
        public int UserId { set; get;}
        public string FullName { set; get;}
        public string Password { set; get;}
        public string Email { set; get;}
        public DateTime DateOfBirth { get; set;}
        public string Role { get; set;}

        public ICollection<Advertisement> Advertisements { set; get;}
        public void HashPassword()
        {
            Password = BCrypt.Net.BCrypt.HashPassword(Password);
        }


    }
}
