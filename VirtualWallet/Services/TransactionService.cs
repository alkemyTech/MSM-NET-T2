using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionRepository;

        public TransactionService(ITransactionsRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> getAllTransactionsAsync()
        {
            return await _transactionRepository.getAll();

        }


        public async Task<Transaction> getTransactionAsync(int id)
        {
            return await _transactionRepository.getById(id);
        }

        public async Task addTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.Insert(transaction);
        }

        public async Task updateTransactionAsync(Transaction transaction)
        {
            await _transactionRepository.Update(transaction);
        }

        public async Task deleteTransactionAsync(int id)
        {
            await _transactionRepository.Delete(id);
        }
    }
}
