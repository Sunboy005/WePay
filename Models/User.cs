﻿using Microsoft.AspNetCore.Identity;

namespace wepay.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime DateModified { get; set; } = DateTime.Now;

    }
}
