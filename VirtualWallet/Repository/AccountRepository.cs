using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly VirtualWalletDbContext _dbContext;

    public AccountRepository(VirtualWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Account>> GetAllAccounts()
    {
        return await _dbContext.Accounts
            .ToListAsync();
    }

    public async Task<Account> GetAccountById(int id)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAccount(Account account)
    {
        _dbContext.Accounts.Add(account);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAccount(Account account)
    {
        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAccount(int id)
    {
        var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        if (account != null)
        {
            _dbContext.Accounts.Remove(account);
            await _dbContext.SaveChangesAsync();
        }
    }
}