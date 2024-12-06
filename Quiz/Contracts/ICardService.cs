
using Quiz.Entities;

namespace Quiz.Contracts
{
    public interface ICardService
    {
        Result Authenticating(string cardNumber, string password);
        Card GetBYCardNumber(string cardNumber);
        Result ViewBalance(string cardNumber);
        Result ChangePassword(string cardNumber, string oldPassword, string newPassword);
        string ViewCardOwnerName(string cardNumber);
        

    }

}
