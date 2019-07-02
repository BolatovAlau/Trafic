using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TraficLight.BusinessLogic.Entities
{
    public class Observation
    {
        [Key]
        public int Id { get; set; }
        public string Color { get; set; }
        public string NumberOne { get; set; }
        public string NumberTwo { get; set; }
        public string SequenceId { get; set; }

        public virtual Sequence Sequence { get; set; }
    }
}
