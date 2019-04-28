using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Models.Validation;

namespace Data.Models
{
    public class User
    {
        public string ID { get; set; }
        [Required] [Email] public string Email { get; set; }
        [Required] public string PasswordHash { get; set; }
        [Required] [Name] public string Name { get; set; }
        [Required] public bool IsAdmin { get; set; }
    }
}
