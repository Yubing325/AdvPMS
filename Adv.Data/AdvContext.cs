using Adv.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Adv.Data
{
    public class AdvContext: DbContext
    {
         public AdvContext(DbContextOptions<AdvContext> options)
                :base(options)
        {
            
        }

        public DbSet<Iteration> Iterations { get; set; }
        public DbSet<WorkItem> WorkItems { get; set; }
    }
}