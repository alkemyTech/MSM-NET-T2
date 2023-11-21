using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAll();
    Task<Role> GetById(int id);
    Task Insert(Role role);
    Task Update(Role role);
    Task Delete(int id);
}