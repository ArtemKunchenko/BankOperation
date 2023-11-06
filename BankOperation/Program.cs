using System;
using System.Data.SqlTypes;

namespace BankOperation
{
    internal class Program
    {
        public class BankAccount
        {
            public string AccountNumber { get; set; }
            public double Balance {  get; set; }    
            public BankAccount(string accountNumber, double balance) 
            { 
                AccountNumber = accountNumber;
                Balance = balance;
            }
        }
        static void Main(string[] args)
        {
        }
    }
}
