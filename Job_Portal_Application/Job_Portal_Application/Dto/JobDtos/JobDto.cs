using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobDto
{
    public class JobDto : PostJobDto
    {

        public bool Status { get; set; }
        public Guid JobId { get; set; }

   
            public DateTime DatePosted { get; set; }
        public string CompanyName { get; set; }
        public string TitleName { get; set; }
            public string JobType { get; set; }
    
              public List<string> Skills { get; set; } 
        }


    
}
