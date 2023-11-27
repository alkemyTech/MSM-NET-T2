using VirtualWallet.Models;

namespace VirtualWallet.Services.Interfaces
{
    public interface ICatalogueService
    {
        Task<IEnumerable<Catalogue>> getAllCataloguesAsync();

        Task<Catalogue> getCatalogueAsync(int id);

        Task addCatalogueAsync(Catalogue catalogue);

        Task<Catalogue> updateCatalogueAsync(int id, Catalogue catalogue);

        Task<bool> deleteCatalogueAsync(int id);
    }
}
