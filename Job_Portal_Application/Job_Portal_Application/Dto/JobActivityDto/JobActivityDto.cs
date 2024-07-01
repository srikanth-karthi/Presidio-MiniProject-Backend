using System;

namespace Job_Portal_Application.Dto.JobActivityDto
{
    public class JobActivityDto
    {
        public Guid JobactivityId { get; set; }
        public Guid UserId { get; set; }
        public Guid JobId { get; set; }
        public string Status { get; set; }
        public string? name { get; set; }
        public string? logourl { get; set; }
        public bool ResumeViewed { get; set; }
        public string Comments { get; set; }
        public DateOnly AppliedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
