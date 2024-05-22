using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Models
{
    public class Title
    {
        [Key]
        public Guid TitleId { get; set; } = Guid.NewGuid();


        [StringLength(255)]
        public  string TitleName { get; set; }
    }
}
