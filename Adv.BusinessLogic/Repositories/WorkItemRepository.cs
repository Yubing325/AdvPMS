using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddWorkItem(Guid iterationId, WorkItem workItem)
        {
            if(iterationId == null) throw new ArgumentNullException(nameof(iterationId)); 

            workItem.IterationId = iterationId;
            workItem.Created = DateTime.UtcNow;
            workItem.LastModified = DateTime.UtcNow;

            _context.WorkItems.Add(workItem);
        }

        public async Task<WorkItem> GetWorkItem(Guid id)
        {
            return await _context.WorkItems.FindAsync(id);
        }

        public async Task<IEnumerable<WorkItem>> GetWorkItems(Guid iterationId)
        {
            if(iterationId != null && iterationId != Guid.Empty)
            {
                return await _context.WorkItems
                                        .Where(i => i.IterationId == iterationId)
                                        .ToListAsync();
            }

            return await _context.WorkItems.ToListAsync();

        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0 ;
        }

        
    }
}