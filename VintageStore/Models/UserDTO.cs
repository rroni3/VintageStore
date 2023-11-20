using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageStore.Models
{
    public class UserDTO
    {
        public User User { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
