using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces;

public interface IAccountService
{
    Task<Object> GetAll(int pageNumber, int pageSize, string userId);
    Task<Account> GetById(int id, string userId);
    Task<Account> Insert(AccountDTO accountDto, string userId);
    Task<Account> Deposit(Transaction transaction, string userId);
    Task<Account> Transfer(Transaction transaction, string userId);
    Task<Account> Update(int id, AccountDTO account);
    Task<bool> Delete(int id);   
}