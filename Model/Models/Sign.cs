using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Sign
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Введите название знака")] public string Name { get; set; }
        public byte[] Image { get; set; }
        [Required(ErrorMessage = "Выберите тип знака")] public virtual SignType Type { get; set; }
        [Required(ErrorMessage = "Введите информацию о знаке")] public string Information { get; set; }
        [Required(ErrorMessage = "Введите посказки к знаку")] public string Hints { get; set; }
        public bool ForKids { get; set; }
        public bool ForPedestrians { get; set; }
        public bool ForBikers { get; set; }
        public bool ForDrivers { get; set; }
        
    }
}
