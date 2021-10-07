using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.BLL.WithdrawRules
{
    public class PremiumAccountWithdrawRule : IWithdraw
    {
        public AccountWithdrawResponse Withdraw(Account account, decimal amount)
        {
            AccountWithdrawResponse response = new AccountWithdrawResponse();

            if (account.Type != AccountType.Premium)
            {
                response.Success = false;
                response.Message = $"Error: a non-premium account hit the Premium Withdraw Rule.  Contact IT.";
                return response;
            }

            if (amount >= 0)
            {
                response.Success = false;
                response.Message = $"Withdrawal amounts must be negative!";
                return response;
            }

            if (account.Balance + amount < -500)
            {
                response.Success = false;
                response.Message = $"This amount will overdraft more than your $500 limit!";
                return response;
            }

            response.Success = true;
            response.Account = account;
            response.Amount = amount;
            response.OldBalace = account.Balance;
            account.Balance += amount;

            return response;
        }
    }
}
