using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models.Responses
{
    public class AccountWithdrawResponse : Response
    {
        public Account Account { get; set; }
        public decimal Amount { get; set; }
        public decimal OldBalace { get; set; }
    }
}
