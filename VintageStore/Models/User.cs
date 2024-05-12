using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VintageStore.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserPswd { get; set; }
        public string UserName { get; set; }

         

        public bool IsAdmin()
        {
            return Id == 3;
        }

    }
}
