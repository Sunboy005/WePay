namespace wepay.Models.DTOs
{
    public class AdminCreationDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ProfilePicture { get; set; }

        public string Address { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;

        public string Password { get; set; }
        public IEnumerable<string> Role { get; set; } = new List<string>() { "Admin" };
    }
}
