using BankAppControllers.Models;

namespace BankAppControllers.Data
{
    public interface IAccountService
    {
        List<Account> GetAll();
        Account GetByID(int id);
    }
}
