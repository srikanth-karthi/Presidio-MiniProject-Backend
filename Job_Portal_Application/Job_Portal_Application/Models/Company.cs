using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class Company
    {
        [Key]
        public Guid CompanyId { get; set; } = Guid.NewGuid();
        public string? LogoUrl { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string CompanyName { get; set; }

        [StringLength(400)]
        public string? CompanyDescription { get; set; }

        public Guid CredentialId { get; set; }
        public Credential Credential { get; set; }

        [StringLength(200)]
        public string CompanyAddress { get; set; }

        public string City { get; set; }
            
        public int? CompanySize { get; set; }

        [Url]
        public string? CompanyWebsite { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
