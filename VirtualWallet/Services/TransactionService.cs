using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly VirtualWalletDbContext _dbContext;

        public TransactionService(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Transaction>> getAllTransactionsAsync()
        {
            var transactions = _dbContext.Transactions.ToList();

            return transactions;
        }


        public async Task<Transaction> getTransactionAsync(int transaction_id)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.transactionId == transaction_id);
        }

        public async Task addTransactionAsync(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task updateTransactionAsync(Transaction transaction)
        {
            _dbContext.Transactions.Update(transaction);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteTransactionAsync(int transaction_id)
        {
            var transaction = await getTransactionAsync(transaction_id);

            if (transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
