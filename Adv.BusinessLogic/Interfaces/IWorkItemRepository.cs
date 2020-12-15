using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.Data.Entities;

namespace Adv.BusinessLogic.Interfaces
{
    public interface IWorkItemRepository
    {
        void AddWorkItem(Guid iterationId, WorkItem model);

        Task<IEnumerable<WorkItem>> GetWorkItems(Guid IterationId);

        Task<WorkItem> GetWorkItem(Guid id);

        Task<bool> SaveAllAsync();
        
    }
}