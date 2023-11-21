using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Services;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogueController : Controller
    {
        private readonly CatalogueService _catalogueService;

        public CatalogueController(CatalogueService catalogueService)
        {
            _catalogueService = catalogueService;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var catalogues = await _catalogueService.getAllCataloguesAsync();

            if (catalogues == null)
            {
                return NotFound();
            }

            return Ok(catalogues);

        }
        
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var catalogue = await _catalogueService.getCatalogueAsync(id);
            if (catalogue == null)
            {
                return NotFound();
            }

            return Ok(catalogue);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Catalogue catalogue)
        {
            await _catalogueService.addCatalogueAsync(catalogue);

            return CreatedAtAction("Get", new { id = catalogue.Id }, catalogue);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, Catalogue catalogue)
        {
            var _catalogue = await _catalogueService.getCatalogueAsync(id);

            if (_catalogue == null)
            {
                return NotFound();
            }

            _catalogue.ProductDescription = catalogue.ProductDescription;
            _catalogue.Image = catalogue.Image;
            _catalogue.Points = catalogue.Points;

            await _catalogueService.updateCatalogueAsync(_catalogue);

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var catalogue = await _catalogueService.getCatalogueAsync(id);

            if (catalogue == null)
            {
                return NotFound();
            }

            await _catalogueService.deleteCatalogueAsync(id);

            return Ok();
        }
    }
}
