using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository
{
    public class TransactionRepository : ITransactionsRepository
    {
        private readonly ApiContext _dbContext;

        public TransactionRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> getAll()
        {
            return await _dbContext.Transactions.ToListAsync();
        }

        public async Task<Transaction> getById(int transaction_id)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.transaction_id == transaction_id);

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
            var _transaction = _dbContext.Transactions.FirstOrDefault(t => t.transaction_id == transaction_id);

            if (_transaction != null)
            {
                _dbContext.Transactions.Remove(_transaction);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
