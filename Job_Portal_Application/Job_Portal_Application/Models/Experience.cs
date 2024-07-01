using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_Application.Models
{
    public class Experience
    {
        [Key]
        public Guid ExperienceId { get; set; } = Guid.NewGuid();




        public Guid UserId { get; set; }

        [StringLength(255)]
        public string CompanyName { get; set; }

        public Guid TitleId { get; set; }

        public Title Title { get; set; }

        public DateOnly StartYear { get; set; }

        public DateOnly EndYear { get; set; }

        [NotMapped]
        public int ExperienceDuration
        {
            get
            {
                return EndYear.Year - StartYear.Year;
            }
        }
    }
}
