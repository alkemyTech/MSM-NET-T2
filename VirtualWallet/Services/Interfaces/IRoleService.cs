using VirtualWallet.Models;
using VirtualWallet.Models.DTO;

namespace VirtualWallet.Services.Interfaces;

public interface IRoleService
{
    Task<Object> GetAll(int pageNumber, int pageSize, string userId);
    Task<Role> GetById(int id, string userId);
    Task<Role> Insert(RoleDTO roleDto, string userId);
    Task<Role> Update(int id, RoleDTO role);
    Task<bool> Delete(int id);
}