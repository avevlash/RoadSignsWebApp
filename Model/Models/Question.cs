using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Question
    {
        public int ID { get; set; }
        [Required] public string Text { get; set; }
        [Required] public virtual Sign Sign { get; set; }
        [Required] public virtual List<Variant> Variants { get; set; }
        public bool ForKids { get; set; }
        public bool ForPedestrians { get; set; }
        public bool ForBikers { get; set; }
        public bool ForDrivers { get; set; }
    }
}
