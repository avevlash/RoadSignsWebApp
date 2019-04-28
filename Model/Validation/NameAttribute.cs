using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Validation
{
    public class NameAttribute : RegularExpressionAttribute
    {
        public NameAttribute()
            : base(@"^[A-ЯЁ][а-яё]+\s[A-ЯЁ][а-яё]+$")
        {
            this.ErrorMessage = "Пожалуйста, введите корректные имя и фамилию";
        }
    }
}
