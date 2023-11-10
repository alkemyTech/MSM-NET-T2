using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository;

public class RoleRepository : IRoleRepository
{
    private readonly VirtualWalletDbContext _dbContext;

    public RoleRepository(VirtualWalletDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Role>> GetAllRoles()
    {
        return await _dbContext.Roles
            .ToListAsync();
    }

    public async Task<Role> GetRoleById(int id)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddRole(Role role)
    {
        _dbContext.Roles.Add(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRole(Role role)
    {
        _dbContext.Roles.Update(role);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRole(int id)
    {
        var role = _dbContext.Roles.FirstOrDefault(a => a.Id == id);
        if (role != null)
        {
            _dbContext.Roles.Remove(role);
            await _dbContext.SaveChangesAsync();
        }
    }
}