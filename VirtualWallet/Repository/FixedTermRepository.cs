using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository;

public class FixedTermRepository : IFixedTermRepository
{
    private readonly VirtualWalletDbContext _dbContext;

    public FixedTermRepository(VirtualWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<FixedTermDeposit>> GetAll()
    {
        return await _dbContext.FixedTermDeposits
            .ToListAsync();
    }

    public async Task<IEnumerable<FixedTermDeposit>> GetMyFixedTerms(int id)
    {
        return await _dbContext.FixedTermDeposits
            .ToListAsync();
    }

    public async Task<FixedTermDeposit> GetById(int id)
    {
        return await _dbContext.FixedTermDeposits.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task Insert(FixedTermDeposit fixedTermDeposit)
    {
        _dbContext.FixedTermDeposits.Add(fixedTermDeposit);
        //await _dbContext.SaveChangesAsync();
    }

    public async Task Update(FixedTermDeposit fixedTermDeposit)
    {
        _dbContext.FixedTermDeposits.Update(fixedTermDeposit);
        //await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var fixedTermDeposit = _dbContext.FixedTermDeposits.FirstOrDefault(a => a.Id == id);
        if (fixedTermDeposit != null)
        {
            _dbContext.FixedTermDeposits.Remove(fixedTermDeposit);
            //await _dbContext.SaveChangesAsync();
        }
    }
}