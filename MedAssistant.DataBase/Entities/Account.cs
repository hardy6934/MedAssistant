using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.DataBase.Entities
{
    public class Account : IBaseEntity
    {
        
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

         
        public User Users { get; set; }

        

    }
}
