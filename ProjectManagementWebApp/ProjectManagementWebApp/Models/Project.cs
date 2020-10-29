using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementWebApp.Models
{
    public class Project : EntityBase
    {
        //[Required(ErrorMessage = "You have to set it.")]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Display(Name = "Project description")]
        public string Description { get; set; }

        [InverseProperty(nameof(Task.Project))]
        public ICollection<Task> Tasks { get; set; }
    }
}
