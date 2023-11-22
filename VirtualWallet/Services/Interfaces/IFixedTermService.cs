using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces;

public interface IFixedTermService
{
    Task<IEnumerable<FixedTermDeposit>> GetAll();
    Task<FixedTermDeposit> GetById(int id);
    Task Insert(FixedTermDeposit fixedTermDeposit);
    Task Update(FixedTermDeposit fixedTermDeposit);
    Task Delete(int id);
}