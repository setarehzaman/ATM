
using Quiz.Contracts;
using Quiz.Entities;
using Quiz.Repositories;

namespace Quiz.Services
{
    public class TransactionService : ITransactionService
    {
        #region Props and Ctors
        private readonly ITransactionRepository _transRepository;
        private readonly ICardRepository _cardRepository;

        public TransactionService()
        {
            _transRepository = new TransactionRepository();
            _cardRepository = new CardRepository();

        }
        #endregion

        #region Transfering
        public Result TransferMoney(string sourceCardNumber, string destinationCardNumber, float amount)
        {
            var sourceCard = _cardRepository.GetByCardNumber(sourceCardNumber);
            var destinationCard = _cardRepository.GetByCardNumber(destinationCardNumber);

            float fee = CalculateTransactionFee(amount);
            float totalAmount = amount + fee;

            if (sourceCard.Balance < amount)
                return new Result { IsSuccess = false, Message = "\nNot Enough Balance!" };

            var today = DateTime.Now.Date;
            var dailyTransferredAmount = _transRepository.GetDailyTransferredAmount(sourceCardNumber, today);
            if (dailyTransferredAmount + totalAmount > 250)
                return new Result { IsSuccess = false, Message = "\nyou cant transfer money more than 250$ daily" };

            var transaction = new Transaction
            {
                SourceCardId = sourceCard.Id,
                DestinationCardId = destinationCard.Id,
                SourceCardNumber = sourceCardNumber,
                DestinationCardNumber = destinationCardNumber,
                Amount = amount,
                Fee = fee,  
                TransactionDate = DateTime.Now,
            };

            sourceCard.Balance -= totalAmount;
            destinationCard.Balance += amount;

            bool submitTransfer = _cardRepository.Update(destinationCard);
            if (!submitTransfer)
            {
                sourceCard.Balance += totalAmount;
                transaction.IsSuccessful = false;
                _transRepository.AddTransaction(transaction);
                return new Result { IsSuccess = false, Message = "Tranfer failed. the amount will return within 72h" };
            }
            else
            {
                _cardRepository.Update(sourceCard);
                transaction.IsSuccessful = true;
                _transRepository.AddTransaction(transaction);
                return new Result { IsSuccess = true, Message = "Transfer successful." };
            }
        } 
        public List<Transaction> GetTransactions(string cardNumber)
        {
            return _transRepository.GetTransactionsByCard(cardNumber);
        }
        #endregion

        #region Fee 
        public float CalculateTransactionFee(float amount)
        {
            if (amount >= 1000)
            {
                amount *= 0.015f;
                return amount;
            }
            else
            {
                amount *= 0.005f;
                return amount;
            }
        }
        #endregion

        #region Validation
        public Result ValidDestinationCard(string destinationCardNumber)
        {
            var destinationCard = _cardRepository.GetByCardNumber(destinationCardNumber);

            if (destinationCardNumber.Length != 16 && destinationCard == null)
                return new Result { IsSuccess = false, Message = "\nInvalid destination card." };

            return new Result { IsSuccess = true };
        }
        #endregion

        #region Transaction Code
        private DateTime _codeGenerationTime;
        public Result GenerateTransactionCode(string cardNumber)
        {
            var card = _cardRepository.GetByCardNumber(cardNumber);

            if (card == null || !card.IsActive)
                return new Result { IsSuccess = false, Message = "\nInvalid or Inactive card." };

            var random = new Random();
            int code = random.Next(10000, 99999);

            string filePath = $"TransactionCode_{cardNumber}.txt";

            File.WriteAllText(filePath, code.ToString());

            _codeGenerationTime = DateTime.Now;

            return new Result { IsSuccess = true, Message = $"\nA transaction code sent...  {code}" };
        }
        public Result ValidateTransactionCode(string cardNumber, int enteredCode)
        {
            string filePath = $"TransactionCode_{cardNumber}.txt";

            if (!File.Exists(filePath))
                return new Result { IsSuccess = false, Message = "\nTransaction code not found or expired." };

            string savedCode = File.ReadAllText(filePath);

            if (!int.TryParse(savedCode, out int validCode) || enteredCode != validCode)
                return new Result { IsSuccess = false, Message = "\nInvalid transaction code." };

            if ((DateTime.Now - _codeGenerationTime).TotalMinutes > 5)
            {
                File.Delete(filePath);
                return new Result { IsSuccess = false, Message = "\nThis Transaction code is expired." };
            }

            File.Delete(filePath);
            return new Result { IsSuccess = true, Message = "\nTransaction code Correct!" };
        }
        #endregion
    }
}
