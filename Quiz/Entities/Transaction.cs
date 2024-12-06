
namespace Quiz.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string SourceCardNumber { get; set; }
        public string DestinationCardNumber { get; set; }
        public float Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool IsSuccessful { get; set; }
        public float Fee { get; set; }
        public int SourceCardId { get; set; }
        public int DestinationCardId { get; set; }
        public Card SourceCard { get; set; }
        public Card DestinationCard { get; set; }
    }
}
