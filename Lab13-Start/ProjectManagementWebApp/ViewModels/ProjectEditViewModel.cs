using ProjectManagementWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels
{
    public class ProjectEditViewModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }

        //public string NameAndDescription { get; set; }
        //public int CharCount { get; set; }

        //public ProjectEditViewModel()
        //{
        //}

        //public ProjectEditViewModel(Project project)
        //{
        //    this.ProjectId = project.ID;
        //    this.ProjectName = project.Name;
        //    this.ProjectDescription = project.Description;
        //    this.NameAndDescription = string.Join(' ', project.Name, project.Description);
        //    this.CharCount = project.Name.Count();
        //}

        //public Project Convert()
        //{
        //    return new Project { 
        //        ID = this.ProjectId,
        //        Description = this.ProjectDescription,
        //        Name = this.ProjectName
        //    };

        //}

        //public Project Convert(Project project)
        //{
        //    project.Description = this.ProjectDescription;
        //    project.Name = this.ProjectName;
        //    return project;

        //}

    }
}
