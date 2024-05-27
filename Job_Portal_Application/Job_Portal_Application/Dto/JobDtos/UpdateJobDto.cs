using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobDtos
{
    public class UpdateJobDto
    {

        public Guid JobId { get; set; } 
        public Guid TitleId { get; set; }
        public string JobDescription { get; set; }
        public float Lpa { get; set; }
        public float ExperienceRequired { get; set; }
        public JobType JobType { get; set; } 
        public bool Status { get; set; }
    }
}
