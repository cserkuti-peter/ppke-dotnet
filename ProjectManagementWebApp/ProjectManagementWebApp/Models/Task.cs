using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementWebApp.Models
{
    public class Task : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStatus Status { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Deadline { get; set; }

        [ForeignKey(nameof(AssignedToUser))]
        public int? AssignedToUserId { get; set; }
        public ApplicationUser AssignedToUser { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [InverseProperty(nameof(Comment.Task))]
        public ICollection<Comment> Comments { get; set; }
        [InverseProperty(nameof(FileMeta.Task))]
        public ICollection<FileMeta> UploadedFiles { get; set; }
    }
}
