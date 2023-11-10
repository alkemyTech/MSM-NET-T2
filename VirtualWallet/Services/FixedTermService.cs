using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class FixedTermService : IFixedTermService
    {
        private readonly VirtualWalletDbContext _dbContext;

        public FixedTermService(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<FixedTermDeposit>> getAllFixedTermsAsync()
        {
            var fixedTerms = await _dbContext.FixedTermDeposits.ToListAsync();

            return fixedTerms;
        }


        public async Task<FixedTermDeposit> getFixedTermAsync(int id)
        {
            return await _dbContext.FixedTermDeposits.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task addFixedTermAsync(FixedTermDeposit catalogue)
        {
            _dbContext.FixedTermDeposits.Add(catalogue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task updateFixedTermAsync(FixedTermDeposit catalogue)
        {
            _dbContext.FixedTermDeposits.Update(catalogue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteFixedTermAsync(int id)
        {
            var fixedTerm = await getFixedTermAsync(id);

            if (fixedTerm != null)
            {
                _dbContext.FixedTermDeposits.Remove(fixedTerm);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
