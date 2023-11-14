using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces
{
    public interface IFixedTermService
    {
        Task<IEnumerable<FixedTermDepositDTO>> getAllFixedTermsAsync();

        Task<FixedTermDeposit> getFixedTermAsync(int id);

        Task addFixedTermAsync(FixedTermDeposit fixedTerm);

        Task updateFixedTermAsync(FixedTermDeposit fixedTerm);

        Task deleteFixedTermAsync(int id);
    }
}
