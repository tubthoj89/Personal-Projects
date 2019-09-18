using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Reponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SGBank.Data
{
    public class FileAccountRepository : IAccountRepository
    {
        public FileAccountRepository()
        {

            if (!File.Exists("Accounts.txt"))
            {
                File.Create("Accounts.txt");
            }
        }

        public Account LoadAccount(string AccountNumber)
        {
            List<Account> accounts = RetrieveAccounts(); 
            return accounts.FirstOrDefault(a => a.AccountNumber == AccountNumber);
        }
        

        public void SaveAccount(List<Account> accounts)
        {
            using (StreamWriter streamWriter = new StreamWriter("Accounts.txt"))
            {
                streamWriter.WriteLine("AccountNumber,Name,Balance,Type");
                foreach (var item in accounts)
                {
                    streamWriter.WriteLine(AccountMappers.ToStringCSV(item));
                }
            }
        }

        public void SaveAccount(Account account)
        {
            List<Account> accounts = RetrieveAccounts();
            accounts.FirstOrDefault(a => a.AccountNumber == account.AccountNumber).Balance = account.Balance;
            SaveAccount(accounts);
        }

        public List<Account> RetrieveAccounts()
        {
            List<Account> results = new List<Account>();
            using (StreamReader streamReader = new StreamReader("Accounts.txt"))
            {
                string row = streamReader.ReadLine();
                while ((row = streamReader.ReadLine()) != null)
                {
                    Account accounts = AccountMappers.ToAccount(row);
                    results.Add(accounts);
                }
            }
            return results;
        }

    }
}
