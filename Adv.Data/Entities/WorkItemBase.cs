using System;

namespace Adv.Data.Entities
{
   public class WorkItemBase
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public int Priority { get; set; } = 0;
    }
}