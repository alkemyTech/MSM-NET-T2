using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogueController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public CatalogueController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var catalogues = await _unitOfWork.CatalogueRepo.getAll();

            if (catalogues == null)
            {
                return NotFound("No existen catalogos");
            }

            return Ok(catalogues);

        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var catalogue = await _unitOfWork.CatalogueRepo.getById(id);
            if (catalogue == null)
            {
                return NotFound("Catalogo inexistente");
            }

            return Ok(catalogue);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(Catalogue catalogue)
        {
            var _catalogue = new Catalogue
            {
                Id = catalogue.Id,
                ProductDescription = catalogue.ProductDescription,
                Image = catalogue.Image,
                Points = catalogue.Points,
            };
            await _unitOfWork.CatalogueRepo.Insert(catalogue);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = catalogue.Id }, catalogue);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, Catalogue catalogue)
        {
            var _catalogue = await _unitOfWork.CatalogueRepo.getById(id);

            if (_catalogue == null)
            {
                return NotFound("No se ha encontrado este catalogo");
            }

            _catalogue.ProductDescription = catalogue.ProductDescription;
            _catalogue.Image = catalogue.Image;
            _catalogue.Points = catalogue.Points;

            await _unitOfWork.CatalogueRepo.Update(_catalogue);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var catalogue = await _unitOfWork.CatalogueRepo.getById(id);

            if (catalogue == null)
            {
                return NotFound();
            }

            await _unitOfWork.CatalogueRepo.Delete(id);

            await _unitOfWork.SaveChangesAsync();
            return Ok("Catalogo eliminado con exito");
        }
    }
}
