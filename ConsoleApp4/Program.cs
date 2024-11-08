using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public delegate void AccountEventHandler(string message);

public class CreditCard
{
    public string CardNumber { get; set; }
    public string OwnerName { get;  set; }
    public DateTime ExpirationDate { get; set; }
    private int Pin { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal Balance { get; set; }

    public event AccountEventHandler OnDeposit;
    public event AccountEventHandler OnWithdraw;
    public event AccountEventHandler OnCreditUsageStart;
    public event AccountEventHandler OnTargetBalanceReached;
    public event AccountEventHandler OnPinChanged;

    public CreditCard(string cardNumber, string ownerName, DateTime expirationDate, int pin, decimal creditLimit, decimal initialBalance)
    {
        CardNumber = cardNumber;
        OwnerName = ownerName;
        ExpirationDate = expirationDate;
        Pin = pin;
        CreditLimit = creditLimit;
        Balance = initialBalance;
    }

    public void ShowCardInfo()
    {
        Console.WriteLine($"\nCard Number: {CardNumber}");
        Console.WriteLine($"Owner: {OwnerName}");
        Console.WriteLine($"Expiration Date: {ExpirationDate.ToShortDateString()}");
        Console.WriteLine($"Current Balance: ${Balance}");
        Console.WriteLine($"Credit Limit: ${CreditLimit}");
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            OnDeposit?.Invoke($"Deposited ${amount}. New Balance: ${Balance}");
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
        }
    }

    public void Withdraw(decimal amount)
    {
        if (amount > 0 && amount <= Balance + CreditLimit)
        {
            Balance -= amount;
            if (Balance < 0)
            {
                OnCreditUsageStart?.Invoke($"You've started using credit. Amount withdrawn: ${amount}. Balance: ${Balance}");
            }
            else
            {
                OnWithdraw?.Invoke($"Withdrawn ${amount}. New Balance: ${Balance}");
            }
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    public void CheckTargetBalance(decimal targetBalance)
    {
        if (Balance >= targetBalance)
        {
            OnTargetBalanceReached?.Invoke($"Target balance of ${targetBalance} reached.");
        }
        else
        {
            Console.WriteLine($"You need ${targetBalance - Balance} more to reach your target.");
        }
    }

    public void ChangePin(int newPin)
    {
        if (newPin != Pin)
        {
            Pin = newPin;
            OnPinChanged?.Invoke($"PIN successfully changed to {newPin}.");
        }
        else
        {
            Console.WriteLine("New PIN cannot be the same as the current PIN.");
        }
    }
}
