using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Dto.Enums;


namespace Job_Portal_Application.Models
{
    public class Job
    {
        [Key]
        public Guid JobId { get; set; } = Guid.NewGuid();


        public Guid CompanyId { get; set; }

        public Company Company { get; set; }

        public JobType JobType { get; set; }



        public Guid TitleId { get; set; }

        public Title Title { get; set; }




        public float? ExperienceRequired { get; set; }


        public float? Lpa { get; set; }
        public DateOnly DatePosted { get; set; }= DateOnly.FromDateTime(DateTime.Now);

        [StringLength(1000)]
        public string JobDescription { get; set; }


        public bool Status { get; set; } = true;


        public ICollection<JobSkills> JobSkills { get; set; } = new List<JobSkills>();

        public ICollection<JobActivity> JobActivities { get; set; } = new List<JobActivity>();
    }
}
