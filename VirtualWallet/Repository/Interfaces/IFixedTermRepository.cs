using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces
{
    public interface IFixedTermRepository
    {
        //getAll, getById, insert , delete, update
        Task<IEnumerable<FixedTermDeposit>> getAll();

        Task<FixedTermDeposit> getById(int id);

        Task Insert(FixedTermDeposit fixedTerm);

        Task Update(FixedTermDeposit fixedTerm);

        Task Delete(int fixedTerm_id);
    }
}
