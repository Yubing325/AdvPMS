using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.Data.Entities;

namespace Adv.Data.Interfaces
{
    public interface IIterationRepository
    {
        Task<IEnumerable<Iteration>> GetIterationsAsync();

        Task<Iteration> GetIterationByIdAsync(Guid id);

        void CreateIteration(Iteration model);

        Task<bool> SaveAllAsync();

        void Update(Iteration model);

        void DeleteIterationAsync(Iteration iteration);

        bool IterationExists(Guid id);
    }
}