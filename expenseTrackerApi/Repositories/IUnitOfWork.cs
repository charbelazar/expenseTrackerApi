namespace expenseTrackerApi.Repositories
{
    public interface IUnitOfWork
    {
        public ICategoryRepo Categories { get; set; }
        public ICurrencyRepo Currencies { get; set; }       
        public IWalletRepo Wallets { get; set; }
        public IUserRepo Users { get; set; }
        public ITransactionRepo Transactions { get;set; }
        public IReportingRepo Reporting { get; set; }
    }
}
