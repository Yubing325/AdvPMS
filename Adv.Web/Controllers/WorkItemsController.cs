using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adv.BusinessLogic.Services;
using Adv.Data;
using Adv.Data.Entities;
using Adv.Data.Enums;
using Adv.Data.Interfaces;
using Adv.Web.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adv.Web.Controllers
{
    [Route("api/iterations/{iterationId}/workitems")]
    public class WorkItemsController : ApiBaseController
    {
        private readonly WorkItemService _workItemService;
        private readonly IWorkItemRepository _workItemRepository;
        private readonly AdvContext _context;

        private readonly IMapper _mapper;
        public WorkItemsController( WorkItemService workItemService,
                                    IWorkItemRepository workItemRepository, 
                                    AdvContext context,        
                                    IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _workItemService = workItemService;
            _workItemRepository = workItemRepository;
        }

    [HttpGet("/api/workitems")]
    public async Task<ActionResult> GetAllWorkItems()
    {
        var result = await _workItemService.GetAllWorkItems();
        return Ok(result);
    }

    [HttpGet(Name = "GetWorkItemsForIteration")]
    public async Task<ActionResult<IEnumerable<WorkItemDto>>> GetWorkItems(Guid iterationId)
    {
        var workitems = await _workItemRepository.GetWorkItems(iterationId);
        
        var result = _mapper.Map<IEnumerable<WorkItemDto>>(workitems);
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetWorkItem")]
    public async Task<ActionResult<WorkItemDto>> GetWorkItem(Guid iterationId, Guid id)
    {
        var workItem = await _workItemRepository.GetWorkItem(id);

        if (workItem == null) return NotFound("Work Item Not Found");

        var result = _mapper.Map<WorkItemDto>(workItem);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> CreateWorkItem(Guid iterationId, WorkItemCreationDto model)
    {
        var workItemEntity = _mapper.Map<WorkItem>(model);

        _workItemRepository.AddWorkItem(iterationId, workItemEntity);

        if (await _workItemRepository.SaveAllAsync())
        {
            var workItemToReturn = _mapper.Map<WorkItemDto>(workItemEntity);
            return CreatedAtRoute("GetWorkItem", new { iterationId = iterationId, id = workItemEntity.Id }, workItemToReturn);
        }

        return BadRequest();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkItem(Guid iterationId, Guid id, WorkItem workItem)
    {
        if (id != workItem.Id)
        {
            return BadRequest($"workItem id : {workItem.Id} is not matched to {id}");
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

    [HttpPut("/api/workitems/{id}/{state}")]
    public async Task<IActionResult> UpdateWorkItemState(Guid id, int state)
    {
        var workItem = await _workItemRepository.GetWorkItem(id);

        if(workItem == null) return NotFound(nameof(UpdateWorkItemState) 
                                                + $"Workitem {id} is not found in our system");

        workItem.State = (WorkItemState)state;

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
        if (iterationId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(iterationId));
        }

        return _context.Iterations.Any(e => e.Id == iterationId);
    }
}
}