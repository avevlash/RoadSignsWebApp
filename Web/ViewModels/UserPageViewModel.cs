using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;

namespace Web.ViewModels
{
    public class UserPageViewModel
    {
        public string UserName { get; set; }
        public int AnsPercent { get; set; }
        public List<Test> UserTests { get; set; }
    }
}
