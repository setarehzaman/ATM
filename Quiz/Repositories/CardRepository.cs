
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts;
using Quiz.Entities;

namespace Quiz.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDBContext _context;

        public CardRepository()
        {
            _context = new AppDBContext();
        }


        public Card GetByCardNumber(string cardNumber)
        {
            return _context.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
        }

        public Result GetBalance(string cardNumber)
        {
            var card = _context.Cards.FirstOrDefault(c => c.CardNumber == cardNumber);
            if (card == null || !card.IsActive)
            {
                return new Result { IsSuccess = false, Message = "\nInvalid or Inactive card." };

            }
            return new Result { IsSuccess = true, Message = $"\nYour balance is: ${card.Balance}" };
        }

        public bool Update(Card card)
        {
            var existingCard = _context.Cards.FirstOrDefault(c => c.Id == card.Id);
            if (existingCard == null)
            {
                return false;
            }
            existingCard.CardNumber = card.CardNumber;
            existingCard.HolderName = card.HolderName;
            existingCard.Balance = card.Balance;
            existingCard.IsActive = card.IsActive;
            existingCard.Password = card.Password;
            existingCard.UserId = card.UserId;

            _context.SaveChanges();
            return true;
        }

        public string ViewCardOwnerName(string cardNumber)
        {
            var card = _context.Cards
            .Include(c => c.User)
            .FirstOrDefault(c => c.CardNumber == cardNumber);

            if (card.User.Name == null)
            {
                return null;
            }
            return card.User.Name;
        }
    }
}
