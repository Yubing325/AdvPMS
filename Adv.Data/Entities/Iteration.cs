using System;

namespace Adv.Data.Entities
{
    public class Iteration
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
    }
}