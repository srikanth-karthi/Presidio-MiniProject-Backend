namespace Job_Portal_Application.Dto.JobActivityDto
{
    public class UserAppliedJobsDto
    {

        public Guid JobactivityId { get; set; }
        public Guid JobId { get; set; }

        public string JobType { get; set; }
        public string TitleName { get; set; }
        public string CompanyName { get; set; }
        public string JobStatus { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public string? logourl { get; set; }
        public bool ResumeViewed { get; set; }
        public string Applicationstatus  { get; set; }
        public string Comments { get; set; }
        public DateOnly AppliedDate { get; set; }
    }
}
