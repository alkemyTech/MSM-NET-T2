using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly ICatalogueRepository _catalogueRepository;

        public CatalogueService(ICatalogueRepository catalogueRepository)
        {
            _catalogueRepository = catalogueRepository;
        }

        public async Task<IEnumerable<Catalogue>> getAllCataloguesAsync()
        {
            return await _catalogueRepository.getAll();

        }

        public async Task<Catalogue> getCatalogueAsync(int id)
        {
            return await _catalogueRepository.getById(id);
        }

        public async Task addCatalogueAsync(Catalogue catalogue)
        {
            await _catalogueRepository.Insert(catalogue);
        }

        public async Task updateCatalogueAsync(Catalogue catalogue)
        {
            await _catalogueRepository.Update(catalogue);
        }

        public async Task deleteCatalogueAsync(int id)
        {
            await _catalogueRepository.Delete(id);
        }
    }
}