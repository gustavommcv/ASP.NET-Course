using BankAppControllers.Models;

namespace BankAppControllers.Data
{
    public class AccountService : IAccountService
    {
        private readonly List<Account> _accounts;

        public AccountService()
        {
            _accounts = new List<Account>()
            {
                new Account()
                {
                    accountNumber = 1001,
                    accountHolderName = "Gustavo",
                    currentBalance = 10
                }
            };
        }

        public Account GetByID(int id)
        {
            return _accounts.FirstOrDefault(account => account.accountNumber == id);
        }

        public List<Account> GetAll()
        {
            return _accounts;
        }
    }
}
