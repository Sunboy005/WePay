using System.ComponentModel.DataAnnotations;

namespace wepay.Models.DTOs
{
    public class UserUpdateDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(11, ErrorMessage ="Phone Number should be 11 digits")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; set; }
    }
}
