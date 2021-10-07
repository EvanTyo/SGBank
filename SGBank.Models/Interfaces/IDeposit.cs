using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Models.Interfaces
{
    public interface IDeposit
    {
        AccountDepositResponse Deposit(Account account, decimal amount);

    }
}
