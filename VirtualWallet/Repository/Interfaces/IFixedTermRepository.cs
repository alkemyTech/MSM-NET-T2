using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces;

public interface IFixedTermRepository
{
    Task<IEnumerable<FixedTermDeposit>> GetAll();

    Task<IEnumerable<FixedTermDeposit>> GetMyFixedTerms(int id);
    Task<FixedTermDeposit> GetById(int id);
    Task Insert(FixedTermDeposit fixedTermDeposit);
    Task Update(FixedTermDeposit fixedTermDeposit);
    Task Delete(int id);

}