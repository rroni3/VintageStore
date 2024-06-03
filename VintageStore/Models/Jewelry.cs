using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageStore.Models
{
    public class Jewelry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Photo { get; set; }

        public int? Price { get; set; }

        public int CategoryId { get; set; }
    }
}
