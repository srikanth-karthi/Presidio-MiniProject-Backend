using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_Application.Models
{
    public class Education
    {
        [Key]
        public Guid EducationId { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        public DateOnly StartYear { get; set; }

        public DateOnly? EndYear { get; set; }

        [Range(0, 100)]
        public float Percentage { get; set; }

        [Required]
        [StringLength(255)]
        public string InstitutionName { get; set; }

        public bool IsCurrentlyStudying { get; set; }
    }
}
