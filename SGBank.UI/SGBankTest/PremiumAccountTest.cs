using NUnit.Framework;
using SGBank.BLL.DepositRules;
using SGBank.BLL.WithdrawRules;
using SGBank.Models;
using SGBank.Models.Interfaces;
using SGBank.Models.Reponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGBankTest
{
    [TestFixture]
    public class PremiumAccountTest
    {
        [TestCase ("123123", "Premium Account", 500, AccountType.Free, -100, 100, false)]
        [TestCase ("123123", "Premium Account", 500, AccountType.Premium, 100, 100, false)]
        [TestCase ("123123", "Premium Account", 500, AccountType.Premium, -100, 400, true)]
        [TestCase ("123123", "Premium Account", 500, AccountType.Premium, -550, -60, true)]
        public void PremiumAccountWithdrawRuleTest(string accountNumber, string name, decimal balance, AccountType accountType, decimal amount, decimal newbalance, bool expectedResult)
        {
            IWithdraw withdraw = new PremiumAccountWithdrawRule();

            Account account = new Account()
            {
                Name = name,
                AccountNumber = accountNumber,
                Balance = balance,
                Type = accountType
            };

            AccountWithdrawResponse response = withdraw.Withdraw(account, amount);

            Assert.AreEqual(expectedResult, response.Success);

            if (response.Success == true)
            {
                Assert.AreEqual(newbalance, response.Account.Balance);
            }
        }
        [TestCase("33333", "Basic Account", 100, AccountType.Free, 250, false)]
        [TestCase("33333", "Basic Account", 100, AccountType.Premium, -100, false)]
        [TestCase("33333", "Basic Accoung", 100, AccountType.Premium, 250, true)]
        public void PremiumAccountDepositRuleTest(string accountNumber, string name, decimal balance, AccountType acountType, decimal amount, bool expectedResult)
        {
            IDeposit deposit = new NoLimitDepositRule();

            Account account = new Account()
            {
                AccountNumber = accountNumber,
                Name = name,
                Balance = balance,
                Type = acountType
            };

            AccountDepositResponse response = deposit.Deposit(account, amount);

            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
