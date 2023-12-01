using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FixedTermController : ControllerBase
    {
        private readonly FixedTermService _fixedTermService;

        public FixedTermController(FixedTermService fixedTermService)
        {
            _fixedTermService = fixedTermService;
        }

        //ADMIN//
        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                //var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.GetAll(pageNumber, pageSize);

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
        
        //REGULAR//
        [HttpGet]
        [Route("GetAllMyFixedTerms")]

        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetAllMyFixedTerms(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.GetAllMyFixedTerms(pageNumber, pageSize, userId);
              

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
        
        //ADMIN//
        [HttpGet]
        [Route("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                //var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.GetById(id);

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
        
        //REGULAR//
        [HttpGet]
        [Route("GetMyFixedTermById/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> GetMyFixedTermById(int id)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.GetMyFixedTermById(id, userId);

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
        
        //ADMIN
        [HttpPost]
        [Route("Post")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post(FixedTermDepositDTO fixedTermDepositDTO)
        {
            try
            {
                var result = await _fixedTermService.Post(fixedTermDepositDTO);

                if (result == null)
                {
                    throw new Exception("BAD_REQUEST");
                }

                return CreatedAtAction("Get", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }
        
        //REGULAR
        [HttpPost]
        [Route("PostMyNewFixedTerm")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> InsertMyNewFixedTerm(FixedTermDepositDTO fixedTermDepositDTO)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.InsertMyNewFixedTerm(fixedTermDepositDTO, userId);

                if (result == null)
                {
                    throw new Exception("BAD_REQUEST");
                }

                return CreatedAtAction("Get", new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        //ADMIN//
        [HttpPut]
        [Route("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, FixedTermDepositDTO fixedTermDepositDTO)
        {
            try
            {
                var result = await _fixedTermService.Update(id, fixedTermDepositDTO);

                if (result == null)
                {
                    throw new Exception("BAD_REQUEST");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        //REGULAR//
        [HttpPut]
        [Route("EditMyFixedTerm/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> UpdateMyFixedTerm(int id, FixedTermDepositDTO fixedTermDepositDTO)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.UpdateMyFixedTerm(id, fixedTermDepositDTO, userId);

                if (result == null)
                {
                    throw new Exception("BAD_REQUEST");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(400, new
                {
                    status = 400,
                    errors = new[] { new { error = ex.Message } }
                });
            }
        }

        //ADMIN//
        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _fixedTermService.Delete(id);

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
        
        //REGULAR//
        [HttpDelete]
        [Route("DeleteMyFixedTerm/{id}")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> DeleteMyFixedTerm(int id)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var result = await _fixedTermService.DeleteMyFixedTerm(id, userId);

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