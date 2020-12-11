using System.Collections.Generic;
using System.Threading.Tasks;
using Adv.Data.Entities;

namespace Adv.BusinessLogic.Interfaces
{
    public interface IIterationRepository
    {
        Task<IEnumerable<Iteration>> GetIterationsAsync();


    }
}