﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public List<Jewelry> OrderItems { get; set; }
        public int TotalPrice {  get; set; }
        public User User { get; set; }
    }
}
