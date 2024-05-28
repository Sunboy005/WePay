﻿using System;
using System.ComponentModel.DataAnnotations;
using wepay.Models;
using wepay.Models.DTOs;

public class UserForRegistrationDto /*: UserCreationDto*/
{
    [Required (ErrorMessage ="First Name is required")]
    public string FirstName { get; set; }
    [Required (ErrorMessage ="Last Name is required")]
    public string LastName { get; set; }
    [Required (ErrorMessage ="User Name is required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Phone Number is required")]
    [StringLength(11, ErrorMessage = "Phone Number must be 11 digits" )]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
