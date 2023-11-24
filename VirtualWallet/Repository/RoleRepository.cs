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

    public async Task<IEnumerable<Role>> GetAll()
    {
        return await _dbContext.Roles
            .ToListAsync();
    }

    public async Task<Role> GetById(int id)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task Insert(Role role)
    {
        _dbContext.Roles.Add(role);
    }

    public async Task Update(Role role)
    {
        _dbContext.Roles.Update(role);
    }

    public async Task Delete(int id)
    {
        var role = _dbContext.Roles.FirstOrDefault(a => a.Id == id);
        if (role != null)
        {
            _dbContext.Roles.Remove(role);
        }
    }
}