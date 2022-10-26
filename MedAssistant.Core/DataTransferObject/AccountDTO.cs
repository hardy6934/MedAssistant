using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistant.Core.DataTransferObject
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

    }
}
