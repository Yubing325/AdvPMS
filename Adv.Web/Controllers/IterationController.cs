using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.BusinessLogic.Interfaces;
using Adv.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Adv.Web.Controllers
{
    public class IterationsController : ApiBaseController
    {
        private readonly IIterationRepository _iterationRepository;
        public IterationsController(IIterationRepository iterationRepository)
        {
            _iterationRepository = iterationRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Iteration>>> GetIterations()
        {
            var result = await _iterationRepository.GetIterationsAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateIteration(Iteration model)
        {
             _iterationRepository.CreateIteration(model);

            if(await _iterationRepository.SaveAllAsync()) return Ok();

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Iteration>> UpdateIteration(Iteration model)
        {
            _iterationRepository.Update(model);

            if(await _iterationRepository.SaveAllAsync()) return Ok(model);

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteIteration(Guid id)
        {
            await _iterationRepository.DeleteIterationAsync(id);

            if(await _iterationRepository.SaveAllAsync()) return Ok();

            return BadRequest();
        }

    }
}