using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces;

public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllRoles(); 
    Task<Role> GetRoleById(int id);
    Task AddRole(Role role);
    Task UpdateRole(Role role);
    Task DeleteRole(int id);
}