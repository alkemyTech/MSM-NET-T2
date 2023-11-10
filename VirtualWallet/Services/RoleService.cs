using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
         _roleRepository = roleRepository;
    }
    
    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        return await _roleRepository.GetAllRoles();
    }
    
     public async Task<Role> GetRoleById(int id)
    {
        return await _roleRepository.GetRoleById(id);
    }

    public async Task AddRole(Role role)
    {
        await _roleRepository.AddRole(role);
    }

    public async Task UpdateRole(Role role)
    {
        await _roleRepository.UpdateRole(role);
    }

    public async Task DeleteRole(int id)
    {
        await _roleRepository.DeleteRole(id);
    }
}
