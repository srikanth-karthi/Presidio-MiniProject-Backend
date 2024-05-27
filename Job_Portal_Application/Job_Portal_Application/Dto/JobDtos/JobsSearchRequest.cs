namespace Job_Portal_Application.Dto.JobDtos
{
    public class JobsSearchRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? Title { get; set; } = null;
        public float? Lpa { get; set; } = null;
        public bool RecentlyPosted { get; set; }=false;
        public List<Guid>? SkillIds { get; set; } = new List<Guid>();
        public float? ExperienceRequired { get; set; }=null;
        public string? Location { get; set; } = null;
        public Guid? CompanyId { get; set; } = null;
    }

}
