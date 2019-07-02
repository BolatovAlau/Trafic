using System.Collections.Generic;

namespace TraficLight.BusinessLogic.Entities
{
    public class Sequence
    {
        public string Id { get; set; }
        public string Start { get; set; }
        public string Missing { get; set; }
        public bool IsNotFirst { get; set; }

        public ICollection<Observation> Observations { get; set; }
    }
}
