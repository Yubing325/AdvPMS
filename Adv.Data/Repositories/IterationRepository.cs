using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adv.Data;
using Adv.Data.Entities;
using Adv.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Adv.Data.Repositories
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
            model.Created = DateTime.UtcNow;
            _context.Iterations.Add(model);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Iteration>> GetIterationsAsync()
        {
            return await _context.Iterations.ToListAsync();
        }

        public void Update(Iteration model)
        {
            _context.Entry(model).State = EntityState.Modified;
        }

        public void DeleteIterationAsync(Iteration iteration)
        {
              _context.Iterations.Remove(iteration);             
        }

        public async Task<Iteration> GetIterationByIdAsync(Guid id)
        {
            var iteration = await _context.Iterations.FindAsync(id);

            return iteration;
        }

        public bool IterationExists(Guid id)
        {
            return _context.Iterations.Any(e => e.Id == id);
        }
    }
}