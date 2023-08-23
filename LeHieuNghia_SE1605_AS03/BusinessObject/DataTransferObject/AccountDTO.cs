using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DataTransferObject
{
    public class AccountDTO
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }


    }
}
