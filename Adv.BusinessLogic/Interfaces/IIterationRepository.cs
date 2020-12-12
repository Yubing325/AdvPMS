using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.Data.Entities;

namespace Adv.BusinessLogic.Interfaces
{
    public interface IIterationRepository
    {
        Task<IEnumerable<Iteration>> GetIterationsAsync();

        void CreateIteration(Iteration model);

        Task<bool> SaveAllAsync();

        void Update(Iteration model);

        Task DeleteIterationAsync(Guid id);
    }
}