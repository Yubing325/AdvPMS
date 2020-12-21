using System.Linq;
using System.Threading.Tasks;
using Adv.Data.Interfaces;

namespace Adv.BusinessLogic.Services
{
    public class WorkItemService
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly IIterationRepository _iterationRepository;
        public WorkItemService(IWorkItemRepository workItemRepository, IIterationRepository iterationRepository)
        {
            _iterationRepository = iterationRepository;
            _workItemRepository = workItemRepository;

        }

        public async Task<object> GetAllWorkItems()
        {
            var workitems = await _workItemRepository.GetWorkItems();

            var iterations = await _iterationRepository.GetIterationsAsync();

            var result = from i in iterations
                         join wi in workitems on i.Id equals wi.IterationId
                         select new 
                         {
                            Id = wi.Id, 
                            Title = wi.Title, 
                            Description = wi.Description, 
                            Iteration = i.Title, 
                            IterationId = i.Id,
                            Created=wi.Created,
                            Priority = wi.Priority,
                            State = wi.State,
                            LastModified=wi.LastModified.Value,
                         };
            return result.ToList();
            
        }


    }
}