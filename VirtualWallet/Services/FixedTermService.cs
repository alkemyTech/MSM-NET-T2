using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Repository;
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

        public async Task<IEnumerable<FixedTermDepositDTO>> getAllFixedTermsAsync()
        {
            var fixedTerms = await _fixedTermRepository.GetAll();
           

            var fixedTermsDTOs = fixedTerms.Select(fixedTerm => new FixedTermDepositDTO
            {
                Id = fixedTerm.Id,
                UserId = fixedTerm.UserId,
                AccountId = fixedTerm.AccountId,
                Amount = fixedTerm.Amount,
                CreationDate = fixedTerm.CreationDate,
                ClosingDate = fixedTerm.ClosingDate,
                NominalRate = fixedTerm.NominalRate,
                State = fixedTerm.State

            });
            return fixedTermsDTOs;
        }

        public async Task<IEnumerable<FixedTermDepositDTO>> getAllFixedTermsByUserIdAsync(string userId)
        {
            var fixedTerms = await _fixedTermRepository.GetAll();

            var list = fixedTerms.Where(t => t.UserId.ToString() == userId);
            
            var fixedTermsDTOs = list.Select(fixedTerm => new FixedTermDepositDTO
            {
                Id = fixedTerm.Id,
                UserId = (int)fixedTerm.UserId,
                AccountId = fixedTerm.AccountId,
                Amount = fixedTerm.Amount,
                CreationDate = fixedTerm.CreationDate,
                ClosingDate = fixedTerm.ClosingDate,
                State = fixedTerm.State
            }); ;
            return fixedTermsDTOs;
        }

        public async Task<FixedTermDeposit> getMyFixedTermAsync(int id)
        {
            return await _fixedTermRepository.GetById(id);
        }

        public async Task<FixedTermDeposit> getFixedTermAsync(int id)
        {
            return await _fixedTermRepository.GetById(id);
        }

        public async Task addFixedTermAsync(FixedTermDeposit fixedTerm)
        {
            await _fixedTermRepository.Insert(fixedTerm);
        }

        public async Task addFixedTermByUserIdAsync(FixedTermDeposit fixedTerm)
        {
            await _fixedTermRepository.Insert(fixedTerm);
        }
        
        public async Task updateFixedTermAsync(FixedTermDeposit fixedTerm)
        {
            await _fixedTermRepository.Update(fixedTerm);
        }

        public async Task updateMyFixedTermAsync(FixedTermDeposit fixedTerm)
        {
            await _fixedTermRepository.Update(fixedTerm);
        }
        
        public async Task deleteFixedTermAsync(int id)
        {
            await _fixedTermRepository.Delete(id);
        }
    }
}
