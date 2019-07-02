using System;
using System.Collections.Generic;

namespace TraficLight.BusinessLogic.Entities
{
    public class Sequence
    {
        public Guid Id { get; set; }

        public ICollection<Observation> Observations { get; set; }
    }
}
