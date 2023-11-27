using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces;

public interface IAccountService
{
    Task<Object> GetAll(int pageNumber, int pageSize, string userId);
    Task<Account> GetById(int id, string userId);
    Task<Account> Insert(AccountDTO accountDto, string userId);
    Task<Account> Deposit(int id, int amount, string userId);
    Task<Account> Transfer(int id, int toAccount, int amount, string userId);
    Task<Account> Update(int id, AccountDTO account);
    Task<bool> Delete(int id);   
}