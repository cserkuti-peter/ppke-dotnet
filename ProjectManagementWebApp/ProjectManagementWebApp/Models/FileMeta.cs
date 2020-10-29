using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementWebApp.Models
{
    public class FileMeta : EntityBase
    {
        public string Description { get; set; }
        public string FileId { get; set; }

        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
