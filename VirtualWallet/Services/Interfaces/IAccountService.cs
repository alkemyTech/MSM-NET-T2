using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAll();
    Task<Account> GetById(int id);
    Task Insert(Account account);
    Task Update(Account account);
    Task Delete(int id);
}