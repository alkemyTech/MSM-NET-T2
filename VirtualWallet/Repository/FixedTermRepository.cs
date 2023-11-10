using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository
{
    public class FixedTermRepository : IFixedTermRepository
    {
        private readonly VirtualWalletDbContext _dbContext;

        public FixedTermRepository(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FixedTermDeposit>> getAll()
        {
            return await _dbContext.FixedTermDeposits.ToListAsync();
        }

        public async Task<FixedTermDeposit> getById(int id)
        {
            return await _dbContext.FixedTermDeposits.FirstOrDefaultAsync(f => f.Id == id);

        }

        public async Task Insert(FixedTermDeposit fixedTerm)
        {
            _dbContext.FixedTermDeposits.Add(fixedTerm);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(FixedTermDeposit fixedTerm)
        {
            _dbContext.FixedTermDeposits.Update(fixedTerm);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var fixedTerm = _dbContext.FixedTermDeposits.FirstOrDefault(f => f.Id == id);

            if (fixedTerm != null)
            {
                _dbContext.FixedTermDeposits.Remove(fixedTerm);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
