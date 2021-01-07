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
        private readonly IIterationRepository _iterationRepository;
        private readonly AdvContext _context;

        private readonly IMapper _mapper;
        public WorkItemsController( WorkItemService workItemService,
                                    IWorkItemRepository workItemRepository,
                                    IIterationRepository iterationRepository, 
                                    AdvContext context,        
                                    IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _workItemService = workItemService;
            _workItemRepository = workItemRepository;
            _iterationRepository = iterationRepository;
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
    public async Task<IActionResult> UpdateWorkItem(Guid iterationId, Guid id, WorkItemUpdateDto workItem)
    {
        if (id != workItem.Id) return BadRequest($"workItem id : {workItem.Id} is not matched to {id}");  

        if (!_iterationRepository.IterationExists(iterationId)) return NotFound("Iteration Not Found");
    

        var workItemFromRepo = await _workItemRepository.GetWorkItem(id);

        if (workItemFromRepo == null) return NotFound("WorkItem Not Found"); 

        _mapper.Map(workItem, workItemFromRepo);

        _workItemRepository.UpdateWorkItem(workItemFromRepo);

        try
        {
            await _workItemRepository.SaveAllAsync();
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
            await _workItemRepository.SaveAllAsync();
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
        if (!_iterationRepository.IterationExists(iterationId))
        {
            return NotFound("Iteration Not Found");
        }

        var workItem = await _workItemRepository.GetWorkItem(id);
        
        if (workItem == null)
        {
            return NotFound();
        }

       _workItemRepository.DeleteWorkItem(workItem);
        
        await _workItemRepository.SaveAllAsync();

        return NoContent();
    }

    [HttpDelete("/api/workitems/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var workItem = await _workItemRepository.GetWorkItem(id);
        
        if (workItem == null)
        {
            return NotFound("Workitem not found");
        }

        _workItemRepository.DeleteWorkItem(workItem);
        
        await _workItemRepository.SaveAllAsync();

        return NoContent();
    }

    private bool WorkItemExists(Guid id)
    {
        return _workItemRepository.WorkItemExists(id);
    }
   
 }
}