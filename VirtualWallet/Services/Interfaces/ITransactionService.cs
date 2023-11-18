using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<Object> getAllTransactionsAsync(int pageNumber, int pageSize, string userId);

        Task<Transaction> getTransactionAsync(int transactionId, string userId);

        Task<Transaction> addTransactionAsync(TransactionDTO transactionDTO, string userId);

        Task<Transaction> updateTransactionAsync(int transactionId, TransactionDTO transaction);

        Task<bool> deleteTransactionAsync(int transactionId);
    }
}