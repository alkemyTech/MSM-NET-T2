using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualWallet.Models;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAll();
    Task<Account> GetById(int id);
    Task Insert(Account account);
    Task Update(Account account);
    Task Delete(int id);

}