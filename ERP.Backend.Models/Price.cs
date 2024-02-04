﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Backend.Models
{
    public class Price
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public DateTime ValidFrom { get; set; }
        public decimal Amount { get; set; }
        public Article? Article { get; set; }
    }
}
