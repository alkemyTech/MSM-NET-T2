using VirtualWallet.Models;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task AddAccount(Account account);
    Task UpdateAccount(Account account);
    Task DeleteAccount(int id);

}