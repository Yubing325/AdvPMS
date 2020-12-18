using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Adv.Data.Entities;

namespace Adv.BusinessLogic.Interfaces
{
    public interface IWorkItemRepository
    {
        void AddWorkItem(Guid iterationId, WorkItem model);

        Task<IEnumerable<WorkItem>> GetWorkItems([Optional] Guid iterationId);

        Task<WorkItem> GetWorkItem(Guid id);

        Task<bool> SaveAllAsync();
        
    }
}