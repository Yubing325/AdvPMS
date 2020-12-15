using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adv.BusinessLogic.Interfaces;
using Adv.Data;
using Adv.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adv.Web.Controllers
{
    [Route("api/iterations/{iterationId}/workitems")]
    public class WorkItemsController : ApiBaseController
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly AdvContext _context;
        public WorkItemsController(IWorkItemRepository workItemRepository, AdvContext context)
        {
            _context = context;
            _workItemRepository = workItemRepository;
        }

        [HttpGet(Name="GetWorkItemsForIteration" )]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItems(Guid IterationId)
        {
            var workitems = await _workItemRepository.GetWorkItems(IterationId);
            return Ok(workitems);
        }

        [HttpGet("{id}", Name = "GetWorkItem")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(Guid IterationId, Guid id)
        {
            var workItem = await _workItemRepository.GetWorkItem(id);

            if (workItem == null) return NotFound("Work Item Not Found");

            return Ok(workItem);
        }

        [HttpPost]
        public async Task<ActionResult<WorkItem>> CreateWorkItem(Guid iterationId, WorkItem model)
        {
            _workItemRepository.AddWorkItem(iterationId, model);

            if (await _workItemRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetWorkItem", new {IterationId=iterationId, id = model.Id }, model);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkItem(Guid iterationId, Guid id, WorkItem workItem)
        {
            if (id != workItem.Id)
            {
                return BadRequest();
            }

            if (!IterationExists(iterationId))
            {
                return NotFound("Iteration Not Found");
            }

            _context.Entry(workItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkItem(Guid iterationId, Guid id)
        {
            if (!IterationExists(iterationId))
            {
                return NotFound("Iteration Not Found");
            }

            var workItem = await _context.WorkItems.FindAsync(id);
            if (workItem == null)
            {
                return NotFound();
            }

            _context.WorkItems.Remove(workItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool WorkItemExists(Guid id)
        {
            return _context.WorkItems.Any(e => e.Id == id);
        }

        private bool IterationExists(Guid iterationId)
        {
            if(iterationId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(iterationId));
            }

            return _context.Iterations.Any(e => e.Id == iterationId);
        }
    }
}