using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.BusinessLogic.Interfaces;
using Adv.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Adv.Web.Controllers
{
    public class IterationController : ApiBaseController
    {
        private readonly IIterationRepository _iterationRepository;
        public IterationController(IIterationRepository iterationRepository)
        {
            _iterationRepository = iterationRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Iteration>>> GetIterations()
        {
            var result = await _iterationRepository.GetIterationsAsync();

            return Ok(result);
        }

    }
}