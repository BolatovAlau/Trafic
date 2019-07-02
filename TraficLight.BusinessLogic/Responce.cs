using System;
using System.Collections.Generic;
using System.Text;

namespace TraficLight.BusinessLogic
{
    public class Answer
    {
        public string Status { get; set; }
        public object Responce { get; set; }
        public string Msg { get; set; }
    }
    public class Responce
    {
        public int[] Start { get; set; }
        public string[] Missing { get; set; }
        public string Sequence { get; set; }
    }
}
