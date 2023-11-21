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
    
    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _accountRepository.GetAll();
    }
    
     public async Task<Account> GetById(int id)
    {
        return await _accountRepository.GetById(id);
    }

    public async Task Insert(Account account)
    {
        await _accountRepository.Insert(account);
    }

    public async Task Update(Account account)
    {
        await _accountRepository.Update(account);
    }

    public async Task Delete(int id)
    {
        await _accountRepository.Delete(id);
    }    
}