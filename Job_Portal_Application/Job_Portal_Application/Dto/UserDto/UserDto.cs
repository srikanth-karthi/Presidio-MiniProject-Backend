using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.UserDto
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public DateTime? Dob { get; set; }
        public string? AboutMe { get; set;}
        public string Email { get; set; }
        public string? logourl { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string  PortfolioLink { get; set; }
        public string  PhoneNumber { get; set; }
        public string  ResumeUrl { get; set; }
    }
}
