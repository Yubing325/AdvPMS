using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.BusinessLogic.Interfaces;
using Adv.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Adv.Web.Controllers
{
    public class WorkItemsController : ApiBaseController
    {
        private readonly IWorkItemRepository _workItemRepository;
        public WorkItemsController(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkItem>>> GetWorkItems()
        {
            return Ok(await _workItemRepository.GetWorkItems());
        }

        [HttpGet("{id}", Name="GetItem")]
        public async Task<ActionResult<WorkItem>> GetWorkItem(Guid id)
        {
            var workItem = await _workItemRepository.GetWorkItem(id);

            if(workItem == null) return NotFound("Work Item Not Found");

            return Ok(workItem);
        }

        [HttpPost]
        public async Task<ActionResult<WorkItem>> CreateWorkItem(Guid iterationId, WorkItem model)
        {

            var workitem = model;
            _workItemRepository.AddWorkItem(iterationId, workitem);

            if(await _workItemRepository.SaveAllAsync()) 
            {
                return CreatedAtRoute("GetItem", new {id = workitem.Id}, workitem);
            }

            return BadRequest();
        }


    }
}