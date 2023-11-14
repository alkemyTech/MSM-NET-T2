using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
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

        public async Task<IEnumerable<TransactionDTO>> getAllTransactionsAsync()
        {
            var transactions = await _transactionRepository.getAll();


            var transactionDTOs = transactions.Select(transaction => new TransactionDTO
            {
                transactionId = transaction.transactionId,
                Amount = transaction.Amount,
                Concept = transaction.Concept,
                Date = transaction.Date,
                Type = transaction.Type,
                AccountId = transaction.AccountId,
                UserId = transaction.UserId,
                ToAccountId = transaction.ToAccountId
        });

            return transactionDTOs;

        }


        public async Task<Transaction> getTransactionAsync(int id)
        {
            var transaction =  await _transactionRepository.getById(id);

            return transaction;
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
