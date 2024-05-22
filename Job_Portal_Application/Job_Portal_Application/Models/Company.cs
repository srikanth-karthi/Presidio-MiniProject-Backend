using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class Company
    {
        [Key]
        public Guid CompanyId { get; set; }=Guid.NewGuid();


        [StringLength(100)]
        public string CompanyName { get; set; } 


        [EmailAddress]
        public string Email { get; set; }


        public byte[] Password { get; set; }

        public byte[] HasCode { get; set; }

        [StringLength(200)]
        public string CompanyAddress { get; set; }

        public string? City { get; set; }

        public int CompanySize { get; set; } 

        [Url]
        public string CompanyWebsite { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
