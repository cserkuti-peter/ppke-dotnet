using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Deadline { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public ApplicationUser User { get; set; }

        [InverseProperty(nameof(FileMeta.Task))]
        public ICollection<FileMeta> UploadedFiles { get; set; }

        [InverseProperty(nameof(Comment.Task))]
        public ICollection<Comment> Comments { get; set; }

    }
}
