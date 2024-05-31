namespace Job_Portal_Application.Dto.JobActivityDto
{

        public class FilteredJobActivitiesDto
        {
            public Guid JobId { get; set; }
            public int PageNumber { get; set; } = 1;
            public int PageSize { get; set; } = 25;
            public bool FirstApplied { get; set; } = false;
            public bool PerfectMatchSkills { get; set; } = false;
            public bool PerfectMatchExperience { get; set; } = false;
            public bool HasExperienceInJobTitle { get; set; } = false;
        }
    

}
