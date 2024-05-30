using Job_Portal_Application.Dto.Enums;

using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class JobActivity
    {

                [Key]
                public Guid JobApplicationId { get; set; }= Guid.NewGuid();
                public Guid UserId { get; set; }
                public User User { get; set; }
                public Guid JobId { get; set; }
                public Job Job { get; set; }

        public JobStatus Status { get; set; } = JobStatus.Applied;
        public bool ResumeViewed { get; set; }=false;

        public string? Comments { get; set; }=string.Empty;
        public DateOnly AppliedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public DateTime? UpdatedDate { get; set; }
        
    }
}
