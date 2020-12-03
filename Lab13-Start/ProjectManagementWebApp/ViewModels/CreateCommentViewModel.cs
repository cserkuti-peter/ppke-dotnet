using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.ViewModels
{
    public class CreateCommentViewModel
    {
        public int TaskId { get; set; }

        public string Comment { get; set; }
    }
}
