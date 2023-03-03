namespace expenseTrackerApi.Models
{
    public class Wallet
    {
       public int Id { get; set; }
        public string Name { get; set; }
        public int currency_id { get; set; }
        public double InitialSum { get; set; }
        public int User_id { get; set; }    
    }
}
