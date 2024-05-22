using Job_Portal_Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class JobActivity
    {

            [Key]
            public Guid UserJobId { get; set; }= Guid.NewGuid();
            public Guid UserId { get; set; }
            public User User { get; set; }
            public Guid JobId { get; set; }
            public Job Job { get; set; }
            public JobStatus Status { get; set; } 
            public DateTime AppliedDate { get; set; }
            public DateTime UpdatedDate { get; set; }
        
    }
}
