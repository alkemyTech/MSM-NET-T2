using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAll();
    Task<Role> GetById(int id);
    Task Insert(Role role);
    Task Update(Role role);
    Task Delete(int id);
}