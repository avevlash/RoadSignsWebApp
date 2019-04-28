using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Statistic
    {
        public int ID { get; set; }
        [Required] public virtual Question Question { get; set; }
        [Required] public bool HasAnswered { get; set; }
        [Required] public virtual Test Test { get; set; }

    }
}
