using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces
{
    public interface IFixedTermService
    {
        Task<Object> GetAll(int pageNumber, int pageSize);
        Task<Object> GetAllMyFixedTerms(int pageNumber, int pageSize, string userId);
        Task<FixedTermDeposit> GetMyFixedTermById(int id, string userId);
        Task<FixedTermDeposit> GetById(int id);
        Task<FixedTermDeposit> InsertMyNewFixedTerm(FixedTermDepositDTO fixedTermDepositDTO, string userId);
        Task<FixedTermDeposit> Post(FixedTermDepositDTO fixedTermDepositDTO);
        Task<FixedTermDeposit> Update(int id, FixedTermDepositDTO fixedTermDepositDTO);
        Task<FixedTermDeposit> UpdateMyFixedTerm(int id, FixedTermDepositDTO fixedTermDepositDTO, string userId);
        Task<bool> Delete(int id);
        Task<bool> DeleteMyFixedTerm(int id, string userId);
    } 
}
