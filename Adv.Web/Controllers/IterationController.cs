using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Adv.Data;
using Adv.Data.Entities;
using Adv.Data.Interfaces;
using Adv.Web.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adv.Web.Controllers
{
    public class IterationsController : ApiBaseController
    {
        private readonly IIterationRepository _iterationRepository;        
        private readonly IMapper _mapper;
        public IterationsController(IIterationRepository iterationRepository, IMapper mapper)
        {
            _mapper = mapper;            
            _iterationRepository = iterationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IterationDto>>> GetIterations()
        {
            var iterations = await _iterationRepository.GetIterationsAsync();

            IEnumerable<IterationDto> result = _mapper.Map<IEnumerable<IterationDto>>(iterations);

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetIteration")]
        public async Task<ActionResult<IterationDto>> GetIteration(Guid id)
        {
            var iteration = await _iterationRepository.GetIterationByIdAsync(id);

            if (iteration == null) return NotFound();

            var result = _mapper.Map<IterationDto>(iteration);

            return result;
        }


        [HttpPost]
        public async Task<ActionResult> CreateIteration(IterationCreationUpdateDto model)
        {
            var iterationEntity = _mapper.Map<Iteration>(model);
            _iterationRepository.CreateIteration(iterationEntity);

            var iterationToReturn = _mapper.Map<IterationDto>(iterationEntity);

            if (await _iterationRepository.SaveAllAsync())
            {
                return CreatedAtRoute("GetIteration", new { id = iterationEntity.Id }, iterationToReturn);
            }

            return BadRequest();
        }
        

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIteration(Guid id)
        {
            var iteration = await _iterationRepository.GetIterationByIdAsync(id);
            
            if (iteration == null)
            {
                return NotFound();
            }

            _iterationRepository.DeleteIterationAsync(iteration);
            
            await _iterationRepository.SaveAllAsync();

            return NoContent();
        }

        private bool IterationExists(Guid id)
        {
            return _iterationRepository.IterationExists(id);
        }

    }
}