using System.ComponentModel.DataAnnotations;

namespace wepay.Models.DTOs
{
    public class UserForChangePasswordDto
    {
        [Required(ErrorMessage ="Email is required", AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        public string UserName {  get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(4, ErrorMessage = "Required code is 4 characters")]
        public string Code { get; set; }


    }
}
