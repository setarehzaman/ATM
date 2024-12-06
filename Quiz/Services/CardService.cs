
using Colors.Net.StringColorExtensions;
using Microsoft.EntityFrameworkCore;
using Quiz.Contracts;
using Quiz.Entities;
using Quiz.Repositories;

namespace Quiz.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _repository;

        public CardService()
        {
            _repository = new CardRepository();

        }

        public Result Authenticating(string cardNumber, string password)
        {
            var card = _repository.GetByCardNumber(cardNumber);

            if (card == null)
                return new Result { IsSuccess = false, Message = "\nCard not found." };

            if (!card.IsActive)
                return new Result { IsSuccess = false, Message = "\nCard is blocked." };

            if (card.Password != password)
            {
                card.FailedAttempts++;
                if (card.FailedAttempts >= 3)
                {
                    card.IsActive = false;
                    _repository.Update(card);
                    return new Result { IsSuccess = false, Message = "\nUnfortunatlly your card Blocked!" };
                }

                _repository.Update(card);
                return new Result { IsSuccess = false, Message = "\nIncorrect password." };
            }

            card.FailedAttempts = 0;
            _repository.Update(card);
            return new Result { IsSuccess = true, Message = "\nLogin successful." };
        }

        public Card GetBYCardNumber(string cardNumber)
        {
            return _repository.GetByCardNumber(cardNumber);
        }
        public Result ViewBalance(string cardNumber)
        {
            var card = _repository.GetByCardNumber(cardNumber);

            if (card == null || !card.IsActive)
                return new Result { IsSuccess = false, Message = "\nInvalid or Inactive card." };

            return new Result { IsSuccess = true, Message = $"\nYour balance is: ${card.Balance}$" };
        }
        public Result ChangePassword(string cardNumber, string oldPassword, string newPassword)
        {
            var card = _repository.GetByCardNumber(cardNumber);

            if (card == null || !card.IsActive)
                return new Result { IsSuccess = false, Message = "\nInvalid or inactive card." };

            if (card.Password != oldPassword)
                return new Result { IsSuccess = false, Message = "\nIncorrect old password." };

            card.Password = newPassword;

            _repository.Update(card);

            return new Result { IsSuccess = true, Message = "\nPassword changed successfully." };
        }
        public string ViewCardOwnerName(string cardNumber)
        {
            return _repository.ViewCardOwnerName(cardNumber).ToString();
        }

    }
}
