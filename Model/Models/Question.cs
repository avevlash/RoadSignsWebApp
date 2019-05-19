using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Models
{
    public class Question
    {
        public int ID { get; set; }
        [Display(Name = "Текст вопроса")][Required(ErrorMessage = "Введите текст вопроса")] public string Text { get; set; }
        public virtual Sign Sign { get; set; }
        [Required(ErrorMessage = "Введите варианты ответа")] public virtual List<Variant> Variants { get; set; }
        public bool ForKids { get; set; }
        public bool ForPedestrians { get; set; }
        public bool ForBikers { get; set; }
        public bool ForDrivers { get; set; }
    }
}
