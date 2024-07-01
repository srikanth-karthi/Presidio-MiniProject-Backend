using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.CompanyDtos
{
    public class CompanyRegisterDto
    {

        [StringLength(100, ErrorMessage = "Company name must be between 1 and 100 characters", MinimumLength = 1)]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public string Password { get; set; }

        [StringLength(200, ErrorMessage = "Company address must be at most 200 characters")]
        [Required(ErrorMessage = "CompanyDescription is required")]
        public string CompanyAddress { get; set; }

        public string City { get; set; }



    }
}
