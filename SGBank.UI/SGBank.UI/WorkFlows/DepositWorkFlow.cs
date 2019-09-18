using SGBank.BLL;
using SGBank.Models.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.WorkFlows
{
    public class DepositWorkFlow
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager accountManager = AccountManagerFactory.Create();
            try
            {
                Console.Write("Enter an account number: ");
                string accountNumber = Console.ReadLine();
                
                Console.WriteLine("Enter a deposit amount: ");
                decimal results;
                var amount = decimal.TryParse(Console.ReadLine(), out results);


                AccountDepositResponse response = accountManager.Deposit(accountNumber, results);

                if (response.Success)
                {
                    Console.WriteLine("Deposit completed");
                    Console.WriteLine($"Old number: {response.Account.AccountNumber:c}");
                    Console.WriteLine($"Old balance: {response.OldBalance:c}");
                    Console.WriteLine($"Amount Deposited: {response.Amount:c}");
                    Console.WriteLine($"New balance: {response.Account.Balance:c}");
                }
                else
                {
                    Console.WriteLine("An error occurred: ");
                    Console.WriteLine(response.Message);
                }

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
