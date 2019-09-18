using SGBank.BLL;
using SGBank.Models.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.WorkFlows
{
    public class AccountLookupWorkFlow
    {
        public void Execute()
        {
            AccountManager manager = AccountManagerFactory.Create();

            Console.Clear();

            Console.WriteLine("Lookup an account");
            Console.WriteLine("----------------------------");
            Console.Write("Enter an account number: ");
            string accountNumber = Console.ReadLine();

            AccountLookupResponse response = manager.LookupAccount(accountNumber);

            if (response.Success)
            {
                ConsoleIO.DisplayAccountDetail(response.Account);
            }
            else
            {
                Console.WriteLine("An error occur: ");
                Console.WriteLine(response.Message);
            }
            Console.WriteLine("Press any key to continue.....");
            Console.ReadKey();
        }
    }
}
