using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    // Cambiamos el service por el UnitOfWork
    /*private readonly IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }*/

    private readonly UnitOfWork _unitOfWork;
    public AccountController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    // GET: api/accounts
    [HttpGet]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> Get()
    {
        //var accounts = await _accountService.GetAllAccounts();
        var accounts = await _unitOfWork.AccountRepo.GetAll();

        if (accounts == null)
        {
            return NotFound();
        }
        return Ok(accounts);
    }

    // GET: api/accounts/{id}
    [HttpGet]
    [Route("{id}")]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        //var account = await _accountService.GetAccountById(id);
        var accounts = await _unitOfWork.AccountRepo.GetById(id);

        if (accounts == null)
        {
            return NotFound();
        }
        return Ok(accounts);
    }

    // POST: api/accounts
    [HttpPost]
    [Authorize(Roles="Regular")]
    public async Task<IActionResult> Post(AccountDTO accountDto)
    {
        // Verificar campos para agregar una cuenta
        if (accountDto ==  null)
        {
            return BadRequest("La cuenta no posee datos válidos");
        }

        if (accountDto.Id == 0 ||
            accountDto.CreationDate == DateTime.MinValue ||
            accountDto.Money <= 0 ||
            accountDto.UserId == 0 
            )
        {
            return BadRequest("La cuenta no pudo ser creada: uno o más errores encontrados");
        }
        
        var _account = new Account
        {
            Id = accountDto.Id,
            CreationDate = DateTime.Now,
            Money = accountDto.Money,
            IsBlocked = accountDto.IsBlocked,
            UserId = accountDto.UserId
        };
        
        //await _accountService.AddAccount(_account);
        await _unitOfWork.AccountRepo.Insert(_account);
        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction("Get", new { id = accountDto.Id }, accountDto);
    }

    // POST api/accounts/{id}
    [HttpPost]
    [Route("{id}")]
    //[Authorize(Roles = "Regular")]
    public async Task<IActionResult> Post(int id, int monto)
    {
        // Realizar un depósito en la cuenta
        var accounts = await _unitOfWork.AccountRepo.GetById(id);
        var user = await _unitOfWork.UserRepo.GetById(id);
        
        var userId = User.FindFirstValue("Id");

        if (accounts == null)
        {
            return NotFound();
        }
        
        // Comparación de el UserId que llega con el UserId de la Account
        if (userId.Equals(accounts.Id.ToString()))
        {
            // Realizar el depósito
            var deposit = accounts.Money += monto;
            
            // Registrar la transacción
            var newTransaction = new Transaction
            { 
                Amount = monto,
                Concept = "Depósito", 
                Date = DateTime.Now, 
                Type = "topup", 
                AccountId = int.Parse(userId), 
                UserId = int.Parse(userId)
                
            };
            await _unitOfWork.TransactionRepo.Insert(newTransaction);
            
            // Calcular los puntos
            var points = (int)Math.Round(monto * 0.02);
            user.Points += points;
            
          await _unitOfWork.SaveChangesAsync();  
        }

        return Ok();
    }
    
    
    // PUT: api/accounts/{id}
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Put(int id, AccountDTO account)
    {
        //var account = await _accountService.GetAccountById(id);
        var _account = await _unitOfWork.AccountRepo.GetById(id);

        if (_account == null)
        {
            return NotFound();
        }
        
        _account.CreationDate = account.CreationDate;
        _account.Money = account.Money;
        _account.IsBlocked = account.IsBlocked;
        
        //await _accountService.UpdateAccount(_account);
        await _unitOfWork.AccountRepo.Update(_account);
        await _unitOfWork.SaveChangesAsync();
        return Ok();
    }

    // DELETE: api/accounts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        //var account = await _accountService.GetAccountById(id);
        var account = await _unitOfWork.AccountRepo.GetById(id);
        
        if (account == null)
        {
            return NotFound();
        }
        
        //await _accountService.DeleteAccount(id);
        await _unitOfWork.AccountRepo.Delete(id);
        await _unitOfWork.SaveChangesAsync();  
        return Ok();
    }
}