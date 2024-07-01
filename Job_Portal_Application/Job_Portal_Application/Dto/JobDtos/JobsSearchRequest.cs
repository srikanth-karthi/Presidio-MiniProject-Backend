using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobDtos
{
    public class JobsSearchRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Guid? JobTitleId { get; set; } 

        public float? MinLpa { get; set; }
        public float? MaxLpa { get; set; } 
        public bool? RecentlyPosted { get; set; }
        public List<Guid>? SkillIds { get; set; }
        public float? MinExperience { get; set; } 
        public float? MaxExperience { get; set; }
        public string? Location { get; set; } 
        public Guid? CompanyId { get; set; }
        public JobType? JobType { get; set; }
    }
}
