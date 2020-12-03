using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels.TaskViewModels
{
    public class TaskDetailsViewModel
    {
        public DateTime? Started { get; set; }
        public DateTime? Deadline { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
        public int ProjectId { get; set; }

        public string UserName { get; set; }
        public string ProjectName { get; set; }
    }
}
