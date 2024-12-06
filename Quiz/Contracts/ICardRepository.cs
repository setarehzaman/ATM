
using Quiz.Entities;
using System.Globalization;

namespace Quiz.Contracts
{
    public interface ICardRepository
    {
        
        Result GetBalance(string  cardNumber);
        Card GetByCardNumber(string cardNumber);
        bool Update(Card card);
        string ViewCardOwnerName(string cardNumber);



    }
}
