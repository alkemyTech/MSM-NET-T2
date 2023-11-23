using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces
{
    public interface IFixedTermRepository
    {
        //getAll, getById, insert , delete, update
        Task<IEnumerable<FixedTermDeposit>> GetAll();

        Task<FixedTermDeposit> GetById(int id);

        Task<FixedTermDeposit> GetMyFixedTermById(int id);

        Task<IEnumerable<FixedTermDeposit>> GetAllByUserId(string userId);

        Task Insert(FixedTermDeposit fixedTerm);

        Task Update(FixedTermDeposit fixedTerm);

        Task Delete(int fixedTerm_id);
    }
}