
using Quiz.Entities;

namespace Quiz.Contracts
{
    public interface ITransactionRepository
    { 
        void AddTransaction(Transaction transaction);
        List<Transaction> GetTransactionsByCard(string cardNumber);
        float GetDailyTransferredAmount(string cardNumber, DateTime date);
    }
}
