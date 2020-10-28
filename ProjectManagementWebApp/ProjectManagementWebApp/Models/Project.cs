using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class Project
    {
        public int ID { get; set; }

        //[Required(ErrorMessage = "You have to set it.")]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Project name")]
        public string Name { get; set; }

        [Display(Name = "Project description")]
        public string Description { get; set; }
    }
}
