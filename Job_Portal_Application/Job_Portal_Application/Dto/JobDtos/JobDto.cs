using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobDto
{
    public class JobDto 
    {

        public bool Status { get; set; }
        public Guid JobId { get; set; }
        public DateTime DatePosted { get; set; }
        public Guid TitleId { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public float? Lpa { get; set; }
        public float? ExperienceRequired { get; set; }
        public string TitleName { get; set; }
        public string  JobType { get; set; }
    
        public List<string> Skills { get; set; } 

    }


    
}
