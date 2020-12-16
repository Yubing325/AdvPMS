using Adv.Data.Enums;

namespace Adv.Web.Dtos
{
    public class WorkItemCreationDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; } = 1;

        public WorkItemState State { get; set; }
    }
}