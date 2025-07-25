﻿namespace SocialNetwork.Core.Application.Dtos.User
{
    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Password { get; set; }
        
    }
}
