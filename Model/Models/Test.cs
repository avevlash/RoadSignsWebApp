using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Test
    {
        public int ID { get; set; }
        [Required] public virtual User User { get; set; }
        public virtual List<Statistic> Stats { get; set; }
    }
}
