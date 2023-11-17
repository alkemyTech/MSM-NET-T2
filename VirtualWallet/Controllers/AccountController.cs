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
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Admin")]
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
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Post(AccountDTO accountDto)
    {
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
    
    // POST api/accounts/Deposit/{id}
    [HttpPost]
    [Route("Deposit/{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Deposit(int id, int amount)
    {
        // Endpoint -> Realizar depósito
        var account = await _unitOfWork.AccountRepo.GetById(id);
        var user = await _unitOfWork.UserRepo.GetById(id);
        var userId = User.FindFirstValue("Id");
        //var userId = "2";
        
        // Verificar la cuenta
        if (account == null)
        {
            return NotFound("No se encuentra / No existe la cuenta solicitada.");
        }
        // Comparación del UserId que llega con el UserId de la Account
        if (userId.Equals(account.Id.ToString()))
        {
            // Realizar el depósito
            var deposit = account.Money += amount;
            
            // Registrar la transacción
            var newTransaction = new Transaction
            { 
                Amount = deposit,
                Concept = "Depósito", 
                Date = DateTime.Now, 
                Type = "topup", 
                AccountId = int.Parse(userId), 
                UserId = int.Parse(userId)
                
            };
            await _unitOfWork.TransactionRepo.Insert(newTransaction);
            
            // Calcular los puntos
            var points = (int)Math.Round(amount * 0.02);
            user.Points += points;
            
          await _unitOfWork.SaveChangesAsync();
        }
        
        return Ok("Depósito exitoso");
    }
    
    // POST api/accounts/Transfer/{id}
    [HttpPost]
    [Route("Transfer/{id}")]
    [Authorize(Roles = "Regular")]
    public async Task<IActionResult> Transfer(int id, int toAccount, int amount)
    {
        // Endpoint -> Realizar transferencia
        var account = await _unitOfWork.AccountRepo.GetById(id);
        var transferId = await _unitOfWork.AccountRepo.GetById(toAccount);
        var user = await _unitOfWork.UserRepo.GetById(id);
        var userId = User.FindFirstValue("Id");
        
        //var userId = "3";
        
        // Verificar ambas cuentas
          if (account == null || transferId == null)
        {
            return NotFound("Una o ambas cuentas no fueron encontradas.");
        }
        // Verificar que la cuenta origen tenga saldo suficiente
        if (account.Money < amount)
        {
            return BadRequest("Saldo insuficiente para realizar la transferencia");
        }
        
        // Comparación del UserId que llega con el UserId de la Account
         if (userId.Equals(account.Id.ToString()))
        {
            // Realizar la transferencia
            var transfer = account.Money -= amount;
            account.Money -= transfer;
            
            var receive = transferId.Money += amount;
            transferId.Money += amount;
                
            // Registrar la transacción de la cuenta emisora
            var newTransaction = new Transaction
            { 
                Amount = transfer,
                Concept = "Transferencia a cuenta de terceros", 
                Date = DateTime.Now, 
                Type = "payment", 
                AccountId = int.Parse(userId), 
                UserId = int.Parse(userId),
                ToAccountId = transferId.Id
            };
            await _unitOfWork.TransactionRepo.Insert(newTransaction);
            
            // Registrar la transacción en la cuenta destino
            var toAccountTransaction = new Transaction
            {
                Amount = receive,
                Concept = "Transferencia de terceros",
                Date = DateTime.Now,
                Type = "payment",
                AccountId = transferId.Id,
                UserId = transferId.UserId
            };
            await _unitOfWork.TransactionRepo.Insert(toAccountTransaction);
            
            // Calcular los puntos de la cuenta emisora
            var points = (int)Math.Round(amount * 0.03);
            user.Points += points;

            await _unitOfWork.AccountRepo.Update(account);
            await _unitOfWork.SaveChangesAsync();
        }
        return Ok("Transferencia realizada con éxito");
    }
  
    // PUT: api/accounts/{id}
    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
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
    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Admin")]
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