using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task AddAccount(Account account);
    Task UpdateAccount(Account account);
    Task DeleteAccount(int id);   
}