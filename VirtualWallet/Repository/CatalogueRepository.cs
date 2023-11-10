using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;

namespace VirtualWallet.Repository
{
    public class CatalogueRepository : ICatalogueRepository
    {
        private readonly VirtualWalletDbContext _dbContext;

        public CatalogueRepository(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Catalogue>> getAll()
        {
            return await _dbContext.Catalogues.ToListAsync();
        }

        public async Task<Catalogue> getById(int id)
        {
            return await _dbContext.Catalogues.FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task Insert(Catalogue catalogue)
        {
            _dbContext.Catalogues.Add(catalogue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Catalogue catalogue)
        {
            _dbContext.Catalogues.Update(catalogue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var _catalogue = _dbContext.Catalogues.FirstOrDefault(c => c.Id == id);

            if (_catalogue != null)
            {
                _dbContext.Catalogues.Remove(_catalogue);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
