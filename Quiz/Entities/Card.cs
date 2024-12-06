
namespace Quiz.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string HolderName { get; set; }
        public float Balance { get; set; }
        public bool IsActive { get; set; } = true;
        public string Password { get; set; }
        public int FailedAttempts { get; set; } = 0;
        public User User { get; set; }
        public int UserId { get; set; }

        public List<Transaction> SourceCardTransactions { get; set; }
        public List<Transaction> DestinationCardTransactions { get; set; }



    }

}
