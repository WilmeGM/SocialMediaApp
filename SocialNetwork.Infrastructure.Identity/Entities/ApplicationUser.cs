﻿using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Phone { get; set; }
    }
}
