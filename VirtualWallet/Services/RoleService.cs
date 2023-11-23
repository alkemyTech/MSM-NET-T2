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

    public async Task<IEnumerable<Role>> GetAll()
    {
        return await _roleRepository.GetAll();
    }

    public async Task<Role> GetById(int id)
    {
        return await _roleRepository.GetById(id);
    }

    public async Task Insert(Role role)
    {
        await _roleRepository.Insert(role);
    }

    public async Task Update(Role role)
    {
        await _roleRepository.Update(role);
    }

    public async Task Delete(int id)
    {
        await _roleRepository.Delete(id);
    }
}