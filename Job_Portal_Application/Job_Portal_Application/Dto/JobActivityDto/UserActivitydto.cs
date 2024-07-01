using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobActivityDto
{
    public class UserActivitydto
    {




            public Guid UserId { get; set; }
        public Guid JobactivityId { get; set; }
        public DateTime? Dob { get; set; }

            public string Email { get; set; }
            public string? logourl { get; set; }
            public string Name { get; set; }
        public DateOnly AppliedDate { get; set; }
        public JobStatus Status { get; set; }


    }

}

