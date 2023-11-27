using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.Models;
using VirtualWallet.Services;
using VirtualWallet.Services.Interfaces;

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
        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _catalogueService.getAllCataloguesAsync();

                if (result == null)
                {
                    throw new Exception("NOT_FOUND");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 404,
                    errors = new[] { new { error = ex.Message } }
                });
            }

        }

        [HttpGet]
        [Route("{id}")]

        [Authorize(Roles = "Admin,Regular")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _catalogueService.getCatalogueAsync(id);

                if (result == null)
                {
                    throw new Exception("NOT_FOUND");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 404,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpPost]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(Catalogue catalogue)
        {
            try
            {
                await _catalogueService.addCatalogueAsync(catalogue);
                return CreatedAtAction("Get", new { id = catalogue.Id }, catalogue);
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, Catalogue catalogue)
        {
            try
            {
                var result = await _catalogueService.updateCatalogueAsync(id,catalogue);

                if(result == null)
                {
                    throw new Exception("NOT_FOUND");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _catalogueService.deleteCatalogueAsync(id);

                if (!result)
                {
                    throw new Exception("NOT_FOUND");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new
                {
                    status = 404,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
    }
}