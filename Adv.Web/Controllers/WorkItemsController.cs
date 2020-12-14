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
    public class WorkItemsController : ApiBaseController
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly AdvContext _context;
        public WorkItemsController(IWorkItemRepository workItemRepository, AdvContext context)
        {
            _context = context;
            _workItemRepository = workItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItems()
        {
            return Ok(await _workItemRepository.GetWorkItems());
        }

        [HttpGet("{id}", Name = "GetItem")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(Guid id)
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
                return CreatedAtRoute("GetItem", new { id = model.Id }, model);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkItem(Guid id, WorkItem workItem)
        {
            if (id != workItem.Id)
            {
                return BadRequest();
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

         // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkItem(Guid id)
        {
            var todoItem = await _context.WorkItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.WorkItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool WorkItemExists(Guid id)
        {
            return _context.WorkItems.Any(e => e.Id == id);
        }
    }
}