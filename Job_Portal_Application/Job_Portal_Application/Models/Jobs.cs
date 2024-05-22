using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Dto;

namespace Job_Portal_Application.Models
{
    public class Job
    {
        [Key]
        public Guid JobId { get; set; } = Guid.NewGuid();

 
        public Guid CompanyId { get; set; }

        public JobType JobType { get; set; }



        public Guid TitleId { get; set; }

        public Title Title { get; set; }


        public ICollection<Skill> SkillsRequired { get; set; }

    
        public float? ExperienceRequired { get; set; } 


        public DateTime DatePosted { get; set; } 

        [StringLength(1000)]
        public string JobDescription { get; set; } 

     
        public bool Status { get; set; }

        public ICollection<JobActivity> JobActivities { get; set; } = new List<JobActivity>();
    }
}
