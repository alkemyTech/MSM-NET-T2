using VirtualWallet.Models;

namespace VirtualWallet.Repository.Interfaces
{
    public interface ICatalogueRepository
    {
        //getAll, getById, insert , delete, update
        Task<IEnumerable<Catalogue>> getAll();

        Task<Catalogue> getById(int id);

        Task Insert(Catalogue catalogue);

        Task Update(Catalogue catalogue);

        Task Delete(int catalogue_id);
    }
}
