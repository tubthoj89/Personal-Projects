using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class PremiumAccountTestRepository : IAccountRepository
    {
        private static Account _account = new Account()
        {
            Name = "Premium Account",
            Balance = 500M,
            AccountNumber = "123123",
            Type = AccountType.Premium
        };

        public Account LoadAccount(string AccountNumber)
        {
            AccountLookupResponse response = new AccountLookupResponse();

            if (AccountNumber == _account.AccountNumber)
            {
                return _account;
            }
            else
            {
                return null;
            }
        }
        public void SaveAccount(Account account)
        {
            _account = account;
        }
    }
}
