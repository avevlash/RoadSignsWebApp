﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
    public class AddSignViewModel
    {
        public Sign Sign { get; set; }
        public List<SignType> Types { get; set; }
        public IFormFile Image { get; set; }
    }
}
