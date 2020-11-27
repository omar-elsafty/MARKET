﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Models.Resources
{
    public class SaveElementResource
    {
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
