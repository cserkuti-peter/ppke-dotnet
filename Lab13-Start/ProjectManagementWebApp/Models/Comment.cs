using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Data { get; set; }

      

        [ForeignKey(nameof(TaskModel))]
        public int TaskId { get; set; }
        public TaskModel Task { get; set; }

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
