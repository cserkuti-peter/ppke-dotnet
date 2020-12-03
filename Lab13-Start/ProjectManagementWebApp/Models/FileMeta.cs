using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public class FileMeta
    {
        [Key]
        public int Id { get; set; }
    
        public string FileName { get; set; }

        [ForeignKey(nameof(Task))]
        public int TaskId { get; set; }
        public TaskModel Task { get; set; }
    }
}
