using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces
{
    public interface IFixedTermService
    {
        Task<IEnumerable<FixedTermDeposit>> getAllFixedTermsAsync();

        Task<FixedTermDeposit> getFixedTermAsync(int id);

        Task addFixedTermAsync(FixedTermDeposit fixedTerm);

        Task updateFixedTermAsync(FixedTermDeposit fixedTerm);

        Task deleteFixedTermAsync(int id);
    }
}
