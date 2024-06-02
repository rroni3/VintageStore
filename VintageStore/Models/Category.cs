using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category(string name)
        {
            this.Name = name;
            if(name== "Necklace") { Id = 1; }
            if(name== "Braclet")  Id = 2;
            if(name== "Earrings") Id= 3;
            if(name== "Rings")Id= 4;
        }
    }

    
}
