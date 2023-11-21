using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Repository.Interfaces
{
    public interface ITransactionsRepository
    {
        //getAll, getById, insert , delete, update
        Task<IEnumerable<Transaction>> getAll();

        Task<Transaction> getById(int transaction_id);

        Task Insert(Transaction transaction);

        Task Update(Transaction transaction);

        Task Delete(int transaction_id);

    }
}
