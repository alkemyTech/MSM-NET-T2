using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class FixedTermService : IFixedTermService
    {
        private readonly IFixedTermRepository _fixedTermRepository;

        public FixedTermService(IFixedTermRepository fixedTermRepository)
        {
            _fixedTermRepository = fixedTermRepository;
        }

        public async Task<IEnumerable<FixedTermDeposit>> getAllFixedTermsAsync()
        {
            return await _fixedTermRepository.getAll();

        }


        public async Task<FixedTermDeposit> getFixedTermAsync(int id)
        {
            return await _fixedTermRepository.getById(id);
        }

        public async Task addFixedTermAsync(FixedTermDeposit fixedTerm)
        {
            await _fixedTermRepository.Insert(fixedTerm);
        }

        public async Task updateFixedTermAsync(FixedTermDeposit fixedTerm)
        {
            await _fixedTermRepository.Update(fixedTerm);
        }

        public async Task deleteFixedTermAsync(int id)
        {
            await _fixedTermRepository.Delete(id);
        }
    }
}
