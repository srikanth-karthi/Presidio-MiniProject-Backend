using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.UserDto
{
    public class UpdateUserDto
    {
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string? Name { get; set; }
        [MaxLength(400, ErrorMessage = "About-me cannot exceed 400 characters")]
        public string? Aboutme { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string? City { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(200, ErrorMessage = "Portfolio link cannot exceed 200 characters")]
        public string? PortfolioLink { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 characters")]
        public string? PhoneNumber { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        [MaxLength(200, ErrorMessage = "Resume URL cannot exceed 200 characters")]
        public string? ResumeUrl { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Dob { get; set; }
    }
}
