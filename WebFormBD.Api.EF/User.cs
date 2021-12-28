using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFormBD.Api.EF
{
    public  class User
    {
        public int  UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public  string Patronumic { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
