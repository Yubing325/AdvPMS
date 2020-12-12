using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.BusinessLogic.Interfaces;
using Adv.Data;
using Adv.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adv.BusinessLogic.Repositories
{
    public class IterationRepository : IIterationRepository
    {
        private readonly AdvContext _context;

        public IterationRepository(AdvContext context)
        {
            _context = context;

        }

        public void CreateIteration(Iteration model)
        {
            var iteration = new Iteration
            {
                Title = model.Title,
                Created = DateTime.UtcNow,
            };

            _context.Iterations.Add(iteration);
            
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Iteration>> GetIterationsAsync()
        {
            return await _context.Iterations.ToListAsync();
        }
    }
}