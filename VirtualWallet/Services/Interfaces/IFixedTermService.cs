using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces
{
    public interface IFixedTermService
    {
        Task<IEnumerable<FixedTermDepositDTO>> getAllFixedTermsAsync();

        Task<FixedTermDeposit> getFixedTermAsync(int id);

        Task<FixedTermDeposit> getMyFixedTermAsync(int id);

        Task<IEnumerable<FixedTermDepositDTO>> getAllFixedTermsByUserIdAsync(string userId);

        Task addFixedTermAsync(FixedTermDeposit fixedTerm);

        Task addFixedTermByUserIdAsync(FixedTermDeposit fixedTerm);

        Task updateFixedTermAsync(FixedTermDeposit fixedTerm);

        Task updateMyFixedTermAsync(FixedTermDeposit fixedTerm);

        Task deleteFixedTermAsync(int id);
    }
}
