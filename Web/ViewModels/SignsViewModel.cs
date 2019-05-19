using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Web.ViewModels
{
    public class SignsViewModel
    {
        public List<Sign> Signs { get; set; }
        public List<SignType> Types { get; set; }
    }
}
