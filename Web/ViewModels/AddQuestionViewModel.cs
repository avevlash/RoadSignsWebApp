using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Web.ViewModels
{
    public class AddQuestionViewModel
    {
        public Question Question { get; set; }
        public  List<Sign> Signs { get; set; }
    }
}
