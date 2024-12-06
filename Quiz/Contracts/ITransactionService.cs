
using Quiz.Entities;

namespace Quiz.Contracts
{
    public interface ITransactionService
    {
        Result TransferMoney(string sourceCardNumber, string destinationCardNumber, float amount);
        List<Transaction> GetTransactions(string cardNumber);
        float CalculateTransactionFee(float amount);
        Result ValidDestinationCard(string destinationCardNumber);
        Result ValidateTransactionCode(string cardNumber, int enteredCode);
        Result GenerateTransactionCode(string cardNumber);
    }
}
