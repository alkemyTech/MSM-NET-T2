using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;


namespace VirtualWallet.Services
{
    public class CatalogueService : ICatalogueService
    {
       
        private readonly UnitOfWork _unitOfWork;

        public CatalogueService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<Catalogue>> getAllCataloguesAsync()
        {
            return await _unitOfWork.CatalogueRepo.getAll();

        }


        public async Task<Catalogue> getCatalogueAsync(int id)
        {
            return await _unitOfWork.CatalogueRepo.getById(id);

        }

        public async Task addCatalogueAsync(Catalogue catalogue)
        {
            await _unitOfWork.CatalogueRepo.Insert(catalogue);
            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<Catalogue> updateCatalogueAsync(int id,Catalogue catalogue)
        {
            var _catalogue = await _unitOfWork.CatalogueRepo.getById(id);

            if(_catalogue == null)
            {
                return null;
            }

            _catalogue.ProductDescription = catalogue.ProductDescription;
            _catalogue.Image = catalogue.Image;
            _catalogue.Points = catalogue.Points;

            await _unitOfWork.CatalogueRepo.Update(_catalogue);
            await _unitOfWork.SaveChangesAsync();

            return _catalogue;
        }

        public async Task<bool> deleteCatalogueAsync(int id)
        {
            var catalogue = await _unitOfWork.CatalogueRepo.getById(id);

            if(catalogue == null)
            {
                return false;
            }

            await _unitOfWork.CatalogueRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
