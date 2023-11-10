using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
         _accountRepository = accountRepository;
    }
    
    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _accountRepository.GetAllAccounts();
    }
    
     public async Task<Account> GetAccountById(int id)
    {
        return await _accountRepository.GetAccountById(id);
    }

    public async Task AddAccount(Account account)
    {
        await _accountRepository.AddAccount(account);
    }

    public async Task UpdateAccount(Account account)
    {
        await _accountRepository.UpdateAccount(account);
    }

    public async Task DeleteAccount(int id)
    {
        await _accountRepository.DeleteAccount(id);
    }    
}