using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobActivityDto
{
    public class UpdateJobactivityDto
    {
        public Guid JobactivityId { get; set; }
        public bool ResumeViewed { get; set; } = false;
        
        public JobStatus status { get; set; } = JobStatus.Applied;

        public string  Comments { get; set; }
    }
}
