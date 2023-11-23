using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    public async Task<IEnumerable<Account>> GetAll()
    {
        return await _dbContext.Accounts
            .ToListAsync();
    }

    public async Task<Account> GetById(int id)
    {
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Insert(Account account)
    {
        _dbContext.Accounts.Add(account);
    }

    public async Task Update(Account account)
    {
        _dbContext.Accounts.Update(account);
    }

    public async Task Delete(int id)
    {
        var account = _dbContext.Accounts.FirstOrDefault(a => a.Id == id);
        if (account != null)
        {
            _dbContext.Accounts.Remove(account);
        }
    }
}