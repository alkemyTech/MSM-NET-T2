using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> getAllTransactionsAsync();

        Task<Transaction> getTransactionAsync(int codTransaction);

        Task addTransactionAsync(Transaction transaction);

        Task updateTransactionAsync(Transaction transaction);

        Task deleteTransactionAsync(int transaction_id);
    }
}
