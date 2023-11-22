using VirtualWallet.Models;
using VirtualWallet.Repository;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services;

public class FixedTermService : IFixedTermService
{
    private readonly IFixedTermRepository _fixedTermRepository;

    public FixedTermService(IFixedTermRepository fixedTermRepository)
    {
        _fixedTermRepository = fixedTermRepository;
    }

    public async Task<IEnumerable<FixedTermDeposit>> GetAll()
    {
        return await _fixedTermRepository.GetAll();
    }

    public async Task<FixedTermDeposit> GetById(int id)
    {
        return await _fixedTermRepository.GetById(id);
    }

    public async Task Insert(FixedTermDeposit fixedTermDeposit)
    {
        await _fixedTermRepository.Insert(fixedTermDeposit);
    }

    public async Task Update(FixedTermDeposit fixedTermDeposit)
    {
        await _fixedTermRepository.Update(fixedTermDeposit);
    }

    public async Task Delete(int id)
    {
        await _fixedTermRepository.Delete(id);
    }
}