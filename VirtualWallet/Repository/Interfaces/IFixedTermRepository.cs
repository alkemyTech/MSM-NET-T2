using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces
{
    public interface IFixedTermRepository
    {

        Task<IEnumerable<FixedTermDeposit>> GetAll();

        Task<FixedTermDeposit> GetById(int id);

        Task Insert(FixedTermDeposit fixedTerm);

        Task Update(FixedTermDeposit fixedTerm);

        Task Delete(int fixedTerm_id);
    }
}