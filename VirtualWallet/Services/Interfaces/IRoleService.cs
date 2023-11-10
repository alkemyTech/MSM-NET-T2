using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<Role>> GetAllRoles();
    Task<Role> GetRoleById(int id);
    Task AddRole(Role role);
    Task UpdateRole(Role role);
    Task DeleteRole(int id); 
}