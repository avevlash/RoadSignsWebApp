using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Sign
    {
        public int ID { get; set; }
        [Required] public string Name { get; set; }
        [Required] public byte[] Image { get; set; }
        [Required] public string Type { get; set; }
        [Required] public string Information { get; set; }
        [Required] public string Hints { get; set; }
        public bool ForKids { get; set; }
        public bool ForPedestrians { get; set; }
        public bool ForBikers { get; set; }
        public bool ForDrivers { get; set; }
        
    }
}
