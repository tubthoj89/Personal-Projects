using SGBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class AccountMappers
    {
        public static Account ToAccount(string row)
        {
            Account account = new Account();
            string[] fields = row.Split(',');
            account.AccountNumber = fields[0];
            account.Name = fields[1];
            account.Balance = decimal.Parse(fields[2]);
            switch (fields[3])
            {
                case "F":
                    account.Type = AccountType.Free;
                    break;
                case "B":
                    account.Type = AccountType.Basic;
                    break;
                case "P":
                    account.Type = AccountType.Premium;
                    break;                
            }
            return account;
        }

        public static string ToStringCSV(Account account)
        {
            string row = $"{account.AccountNumber},{account.Name},{account.Balance},{account.Type.ToString()[0]}";

            return row;
        }
    }
}
