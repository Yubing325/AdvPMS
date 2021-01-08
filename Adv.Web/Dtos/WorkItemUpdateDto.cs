using System;
using Adv.Data.Enums;

namespace Adv.Web.Dtos
{
    public class WorkItemUpdateDto
    {        
        public string Title { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }
        
        public WorkItemState State { get; set; }

        public Guid IterationId { get; set; }
        
    }
}