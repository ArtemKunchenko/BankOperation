using System;
using System.Data.SqlTypes;
using static BankOperation.Program;
using static BankOperation.Program.BankAccount;

namespace BankOperation
{
    public class Program
    {
        #region CLASS BankAccount
        public class BankAccount
        {
            public string AccountNumber { get; set; }
            public double Balance { get; set; }
            public BankAccount(string accountNumber, double balance)
            {
                AccountNumber = accountNumber;
                Balance = balance;
            }
            public delegate void BankOperationHandler(BankAccount bankAccount, double amount, string operationType);
            public event BankOperationHandler BankOperationEvent;
            public void OnBankOperationEvent(double amount, string operationType)
            {
                BankOperationEvent?.Invoke(this, amount, operationType);
            }
            public void ShowAccount() { Console.WriteLine($"Account: {AccountNumber} \nCurrant balance: ${Balance}\n"); }

        }
        #endregion
        #region CLASS TransmitionManager
        public class TransactionManager
        {
            public void Deposit(BankAccount account, double amount)
            {
                account.Balance += amount;
                account.OnBankOperationEvent(amount, "Deposit");
            }

            public void Withdraw(BankAccount account, double amount)
            {
                if (amount <= account.Balance)
                {
                    account.Balance -= amount;
                    account.OnBankOperationEvent(amount, "Withdraw");
                }
            }
        }
        #endregion
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("1234-5678-9101-1121", 10000);
            BankAccount account2 = new BankAccount("1415-1617-1819-2021", 15000);
            TransactionManager transactionManager = new TransactionManager();
            account1.BankOperationEvent += BankOperation;
            account2.BankOperationEvent += BankOperation;
            account1.ShowAccount();
            transactionManager.Deposit(account1, 1000);
            account2.ShowAccount();
            transactionManager.Withdraw(account2, 14000);

            Console.ReadKey();
        }
        static void BankOperation(BankAccount bankAccount, double amount, string operationType)
        {
            Console.WriteLine($"Operation is successful \nAccount: {bankAccount.AccountNumber} \nAmount: ${amount} \nOperation Type: {operationType} \nBalance: ${bankAccount.Balance}\n");
        }
    }
}
