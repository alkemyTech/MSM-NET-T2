using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDTO>> getAllTransactionsAsync();

        Task<Transaction> getTransactionAsync(int codTransaction);

        Task addTransactionAsync(Transaction transaction);

        Task updateTransactionAsync(Transaction transaction);

        Task deleteTransactionAsync(int transaction_id);
    }
}
