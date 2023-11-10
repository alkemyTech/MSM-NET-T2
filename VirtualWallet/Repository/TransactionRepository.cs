using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository
{
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly VirtualWalletDbContext _dbContext;

        public TransactionRepository(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> getAll()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> getById(int transaction_id)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.transactionId == transaction_id);

        }

        public async Task Insert(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Transaction transaction)
        {
            _dbContext.Transactions.Update(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int transaction_id)
        {
            var _transaction = _dbContext.Transactions.FirstOrDefault(t => t.transactionId == transaction_id);

            if (_transaction != null)
            {
                _dbContext.Transactions.Remove(_transaction);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
