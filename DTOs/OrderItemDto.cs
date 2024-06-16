﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class OrderItemDto
    {
       
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantitiy { get; set; }
    }
}
