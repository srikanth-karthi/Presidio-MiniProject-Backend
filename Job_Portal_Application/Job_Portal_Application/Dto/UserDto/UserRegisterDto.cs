using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.UserDto
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string? City { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(200, ErrorMessage = "Portfolio link cannot exceed 200 characters")]
        public string? PortfolioLink { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [MaxLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        public string? Phonenumber { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(200, ErrorMessage = "Resume URL cannot exceed 200 characters")]
        public string? ResumeUrl { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }
    }
}
