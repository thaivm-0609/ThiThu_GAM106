namespace ThiThu.Models
{
    public class GTransaction
    {
        public int transactionID { get; set; }
        public int playerID { get; set; }
        public int itemID { get; set; }
        public DateOnly transactionDate { get; set; }
    }
}
