using System;

namespace ProjectManagementWebApp.Models
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }

        public virtual DateTime Created { get; set; }

        public virtual int CreatedBy { get; set; }
    }
}
