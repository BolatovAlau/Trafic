using System;
using System.Collections.Generic;
using System.Text;

namespace TraficLight.BusinessLogic.Models
{
    public class Request
    {
        public Observation Observation { get; set; }
        public string Sequence { get; set; }
    }
    public class Observation
    {
        public string Color { get; set; }
        public string[] Numbers { get; set; }
    }
}
