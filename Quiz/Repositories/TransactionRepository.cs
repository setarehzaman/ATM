
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts;
using Quiz.Entities;

namespace Quiz.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDBContext _context;

        public TransactionRepository()
        {
            _context = new AppDBContext();
        }    
        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
        public List<Transaction> GetTransactionsByCard(string cardNumber)
        {
            return _context.Transactions
                .Where(t => t.SourceCardNumber == cardNumber || t.DestinationCardNumber == cardNumber)
                .ToList();
        }

        public float GetDailyTransferredAmount(string cardNumber, DateTime date)
        {
            return _context.Transactions
                .Where(t => t.SourceCardNumber == cardNumber && t.TransactionDate.Date == date)
                .Sum(t => t.Amount);
        }

    }
}
