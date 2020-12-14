using System;
using Adv.Data.Enums;

namespace Adv.Data.Entities
{
    public class WorkItem : WorkItemBase
    {
        public WorkItemState State { get; set; }

        public Guid IterationId { get; set; } //?? Guid or String

        public Iteration Iteration { get; set; }
    }
}