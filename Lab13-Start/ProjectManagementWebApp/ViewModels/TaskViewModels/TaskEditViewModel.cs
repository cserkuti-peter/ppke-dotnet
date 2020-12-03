using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels.TaskViewModels
{
    public class TaskEditViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Deadline { get; set; }

        public int ProjectId { get; set; }

        public int? UserId { get; set; }

        public int Id { get; set; }
    }
}
