using expenseTrackerApi.Repositories;

namespace expenseTrackerApi.Classes
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepo Categories {get;set;}
        public ICurrencyRepo Currencies { get;set;}
        public IWalletRepo  Wallets { get;set;}
        public IUserRepo Users { get;set;}
        public ITransactionRepo Transactions { get;set;}
        public IReportingRepo Reporting { get;set;}
        public UnitOfWork(ICategoryRepo categories,
                          ICurrencyRepo currencies,
                          IWalletRepo wallets,
                          IUserRepo users,
                          ITransactionRepo transactions,
                          IReportingRepo reporting)
        {
            Categories = categories;
            Currencies = currencies;
            Wallets = wallets;
            Users = users;
            Transactions = transactions; 
            Reporting = reporting;  
        }
    }
}
