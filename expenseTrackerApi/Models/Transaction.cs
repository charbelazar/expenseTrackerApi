namespace expenseTrackerApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public double Amount { get; set; } 
        public bool IsExpense { get; set; }
        public DateTime TransactionDate { get;set; }
        public int Category_id { get; set; }
        public int Wallet_id { get;set; }

    }
}
