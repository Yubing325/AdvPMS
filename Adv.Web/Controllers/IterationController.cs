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
    public class IterationsController : ApiBaseController
    {
        private readonly IIterationRepository _iterationRepository;
        private readonly AdvContext _context;
        public IterationsController(IIterationRepository iterationRepository, AdvContext context)
        {
            _context = context;
            _iterationRepository = iterationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Iteration>>> GetIterations()
        {
            var result = await _iterationRepository.GetIterationsAsync();

            return Ok(result);
        }

        [HttpGet("{id}", Name="GetIteration")]
        public async Task<ActionResult<Iteration>> GetIteration(Guid id)
        {
            var iteration = await _context.Iterations.FindAsync(id);

            if (iteration == null) return NotFound();

            return iteration;
        }


        [HttpPost]
        public async Task<ActionResult> CreateIteration(Iteration model)
        {
            _iterationRepository.CreateIteration(model);

            if (await _iterationRepository.SaveAllAsync()) 
            {
                return CreatedAtRoute("GetIteration", new {id = model.Id}, model);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Iteration>> UpdateIteration(Guid id, Iteration model)
        {
            if (id != model.Id) return BadRequest();

            _context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IterationExists(id))
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
       public async Task<IActionResult> DeleteIteration(Guid id)
       {
            var iteration = await _context.Iterations.FindAsync(id);
            if (iteration == null)
            {
                return NotFound();
            }

            _context.Iterations.Remove(iteration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IterationExists(Guid id)
        {
            return _context.Iterations.Any(e => e.Id == id);
        }

    }
}