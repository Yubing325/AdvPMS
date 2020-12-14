using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.BusinessLogic.Interfaces;
using Adv.Data;
using Adv.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adv.BusinessLogic.Repositories
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly AdvContext _context;
        public WorkItemRepository(AdvContext context)
        {
            _context = context;

        }

        public void AddWorkItem(Guid iterationId, WorkItem model)
        {
            if(iterationId == null) throw new ArgumentNullException(nameof(iterationId)); 

            WorkItem workItem = new WorkItem
            {
                Title = model.Title,
                Description = model.Description,
                Created = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                Priority = model.Priority,
                State = model.State,
                IterationId = iterationId
            };

            _context.WorkItems.Add(workItem);
        }

        public async Task<WorkItem> GetWorkItem(Guid id)
        {
            return await _context.WorkItems.FindAsync(id);
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItems()
        {
            return await _context.WorkItems.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0 ;
        }
    }
}