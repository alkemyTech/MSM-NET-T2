using Microsoft.EntityFrameworkCore;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly VirtualWalletDbContext _dbContext;

        public CatalogueService(VirtualWalletDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Catalogue>> getAllCataloguesAsync()
        {
            var catalogues = await _dbContext.Catalogues.ToListAsync();

            return catalogues;
        }


        public async Task<Catalogue> getCatalogueAsync(int id)
        {
            return await _dbContext.Catalogues.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task addCatalogueAsync(Catalogue catalogue)
        {
            _dbContext.Catalogues.Add(catalogue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task updateCatalogueAsync(Catalogue catalogue)
        {
            _dbContext.Catalogues.Update(catalogue);
            await _dbContext.SaveChangesAsync();
        }

        public async Task deleteCatalogueAsync(int id)
        {
            var catalogue = await getCatalogueAsync(id);

            if (catalogue != null)
            {
                _dbContext.Catalogues.Remove(catalogue);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
