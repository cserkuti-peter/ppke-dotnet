using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels
{
    public class CommentViewModel
    { 
        public int Id { get; set; }

        public string Comment { get; set; }

        public string Username { get; set; }
        public int TaskId { get; set; }
    }
}
