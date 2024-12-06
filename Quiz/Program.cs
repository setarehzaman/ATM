using Colors.Net;
using Colors.Net.StringColorExtensions;
using ConsoleTables;
using Quiz.Contracts;
using Quiz.Infrastructure;
using Quiz.Services;


ICardService _cardService = new CardService();
ITransactionService _transService = new TransactionService();
void start()
{
    Console.Clear();
    ColoredConsole.WriteLine("*** Wellcome to Setareh's Bank ****".Magenta());
    Console.Write("\nEnter Card Number: ");
    var cardNumber = Console.ReadLine();
    Console.Write("Enter Password: ");
    var password = Console.ReadLine();

    var loginResult = _cardService.Authenticating(cardNumber, password);
    if (loginResult.IsSuccess)
    {
        var card = _cardService.GetBYCardNumber(cardNumber);
        InMemorydb.CurrentCard = card;
        Console.WriteLine(loginResult.Message);
        Console.ReadKey();
        Menu();
    }
    else
    {
        Console.WriteLine(loginResult.Message);
        Console.ReadKey();
        start();
    }
}
void Menu()
{
    bool exit = false;
    do
    {
        Console.Clear();
        ColoredConsole.WriteLine("\n=== Main Menu ===".Magenta());
        Console.WriteLine("1. Transfer Money");
        Console.WriteLine("2. View Transactions");
        Console.WriteLine("3. View Balance");
        Console.WriteLine("4. Change Password");
        Console.WriteLine("0. Exit");
        Console.Write("Select an option: ");
        var choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                Transfer();
                Console.ReadKey();
                break;
            case 2:
                ViewTransactions();
                Console.ReadKey();
                break;
            case 3:
                ViewBalance();
                Console.ReadKey();
                break;
            case 4:
                ChangePassword();
                Console.ReadKey();
                break;
            case 0:
                InMemorydb.CurrentCard = null;
                exit = true;
                ColoredConsole.WriteLine("Thanks For Choosing Our Bank".Yellow());
                break;
            default:
                ColoredConsole.WriteLine("Invalid option. Please try again.".Red());
                break;
        }

    } while (!exit);
}

void ViewTransactions()
{
    var transactions = _transService.GetTransactions(InMemorydb.CurrentCard.CardNumber);

    if (transactions.Count == 0)
    {
        Console.WriteLine("No transactions found.");
        return;
    }
    Console.WriteLine("\n=== Transactions ===");
    ConsoleTable table = new ConsoleTable("ID", "Source", "Destination", "Amount", "Date", "Successful");
    foreach (var transaction in transactions)
    {
        table.AddRow(
            transaction.TransactionId,
            transaction.SourceCardNumber,
            transaction.DestinationCardNumber,
            transaction.Amount,
            transaction.TransactionDate.ToString("yyyy-MM-dd"),
            transaction.IsSuccessful ? "Yes" : "No"
        );
    }
    table.Write(Format.Minimal);
}
void ViewBalance()
{
    var result = _cardService.ViewBalance(InMemorydb.CurrentCard.CardNumber);
    Console.WriteLine(result.Message);
}

void ChangePassword()
{
    Console.Write("Enter your current password: ");
    string oldPassword = Console.ReadLine();

    Console.Write("Enter your new password: ");
    string newPassword = Console.ReadLine();

    var result = _cardService.ChangePassword(InMemorydb.CurrentCard.CardNumber, oldPassword, newPassword);
    Console.WriteLine(result.Message);
}
void Transfer()
{
    Console.Write("\nEnter the destination card number: ");
    string destinationCardNumber = Console.ReadLine();
    var correctDes = _transService.ValidDestinationCard(destinationCardNumber);
    if (correctDes.IsSuccess is false)
    {
        Console.WriteLine(correctDes.Message);
        Console.ReadKey();
        return;
    }
    var nameResult = _cardService.ViewCardOwnerName(destinationCardNumber);

    Console.Write("\nEnter the amount to transfer: ");
    if (!float.TryParse(Console.ReadLine(), out float amount) || amount <= 0)
    {
        Console.WriteLine("\nInvalid amount entered.");
        return;
    }

    var fee = _transService.CalculateTransactionFee(amount);

    Console.WriteLine($"\nTransaction Details:");
    Console.WriteLine($"  Destination Card: {destinationCardNumber}");
    Console.WriteLine($"  Cardholder Name: {nameResult}");
    Console.WriteLine($"  Transfer Amount: ${amount}");
    Console.WriteLine($"  Transaction Fee: ${fee}");
    Console.WriteLine($"  Total Amount : ${amount + fee}");

    Console.WriteLine("Do You Want to Submit? (y/n)");
    var answer = Console.ReadLine();
    if(answer == "y")
    {
        var codeResult = _transService.GenerateTransactionCode(InMemorydb.CurrentCard.CardNumber);
        Console.WriteLine(codeResult.Message);

        Console.Write("Enter the transaction code sent to you: ");
        if (!int.TryParse(Console.ReadLine(), out int enteredCode))
        {
            ColoredConsole.WriteLine("Not Correct".Red());
            return;
        }

        var validationResult = _transService.ValidateTransactionCode(InMemorydb.CurrentCard.CardNumber, enteredCode);
        if (!validationResult.IsSuccess)
        {
            Console.WriteLine(validationResult.Message);
            return;
        }

        var transferResult = _transService.TransferMoney(InMemorydb.CurrentCard.CardNumber, destinationCardNumber, amount);
        Console.WriteLine(transferResult.Message);
    }
    if (answer == "n")
    { 
        ColoredConsole.WriteLine("Transaction Cancelled.".Yellow());
        ColoredConsole.WriteLine("\nReturning To Home Page...".Yellow());
        Console.ReadKey();
        Menu();
    }

}

start();