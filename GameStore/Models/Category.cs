﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Models
{
    public class Category
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string CategoryName { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }
    }
}