using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        CreditCard card = new CreditCard("6989 3979 4969 5959", "Bradley Barcola", new DateTime(2026, 12, 31), 9999, 900, 10000);

        
        card.OnDeposit += LogToConsole;
        card.OnWithdraw += LogToConsole;
        card.OnCreditUsageStart += LogToConsole;
        card.OnTargetBalanceReached += LogToConsole;
        card.OnPinChanged += LogToConsole;

        card.ShowCardInfo();

        Console.WriteLine("\nAttempting to deposit $200...");
        card.Deposit(200);

        Console.WriteLine("\nAttempting to withdraw $50...");
        card.Withdraw(50);

        Console.WriteLine("\nAttempting to withdraw $300...");
        card.Withdraw(300);

        Console.WriteLine("\nChecking if target balance of $500 is met...");
        card.CheckTargetBalance(500);

        Console.WriteLine("\nChanging PIN to 5678...");
        card.ChangePin(5678);

        card.ShowCardInfo();
    }

    static void LogToConsole(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}
