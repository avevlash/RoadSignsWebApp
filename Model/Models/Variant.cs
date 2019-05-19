using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Variant
    {
        public int ID { get; set; }
        [Required] public string Answer { get; set; }
        [Required] public bool IsCorrect { get; set; }
        public virtual Question Question { get; set; }
    }
}
