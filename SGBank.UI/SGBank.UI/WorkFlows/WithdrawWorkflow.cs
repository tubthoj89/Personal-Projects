using SGBank.BLL;
using SGBank.Models;
using SGBank.Models.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBank.UI.WorkFlows
{
    public class WithdrawWorkflow
    {
        public void Execute()
        {
            Console.Clear();

            AccountManager accountManager = AccountManagerFactory.Create();
            try
            {
                Console.WriteLine("Enter Account Number:");
                string accountNumber = Console.ReadLine();

                Console.WriteLine("How much would you like to withdraw?");
                decimal amount = decimal.Parse(Console.ReadLine());

                AccountWithdrawResponse response = accountManager.Withdraw(accountNumber, amount);

                if (response.Success)
                {
                    Console.WriteLine("Withdraw completed");
                    Console.WriteLine($"Old number: {response.Account.AccountNumber:c}");
                    Console.WriteLine($"Old balanace: {response.OldBalance:c}");
                    Console.WriteLine($"Amount Withdraw: {response.Amount:c}");
                    Console.WriteLine($"New balance: {response.Account.Balance:c}");
                }
                else
                {
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(response.Message);
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }

            catch (Exception)
            {
                Console.WriteLine("Wrong input, Try Again!");
                Console.ReadKey();
            }
        }
    }
}
