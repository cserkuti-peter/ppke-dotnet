using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementWebApp.Models
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }

        public virtual DateTime Created { get; set; }

        [ForeignKey(nameof(CreatedByUser))]
        public virtual int? CreatedByUserId { get; set; }
        public virtual ApplicationUser CreatedByUser { get; set; }
    }
}
