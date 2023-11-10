using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAccounts();
    Task<Account> GetAccountById(int id);
    Task AddAccount(Account account);
    Task UpdateAccount(Account account);
    Task DeleteAccount(int id);

}