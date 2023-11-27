using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;

namespace VirtualWallet.Services;

public class AccountService : IAccountService
{
   private readonly UnitOfWork _unitOfWork;

    public AccountService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork; 
    }
    
    public async Task<Object> GetAll(int pageNumber, int pageSize, string userId)
    {
        var accounts = await _unitOfWork.AccountRepo.GetAll();

        var pagedAccounts = accounts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        if (pagedAccounts.Count() == 0)
        {
            return null;
        }
        
        var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;
            
        var nextPage = pageNumber < (int)Math.Ceiling((double)pagedAccounts.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

        var result = new
        {
            Accounts = pagedAccounts,
            prevPage = prevPage,
            nextPage = nextPage
        };
        
        return result;
    }
    
    public async Task<Account> GetById(int id, string userId)
    {
        // Se obtiene la cuenta
        var account = await _unitOfWork.AccountRepo.GetById(id);

        if (account == null)
        {
            return null;
        }

        return account;
    }

    public async Task<Account> Insert(AccountDTO accountDto, string userId)
    {
        var account = new Account
        {
            Id = accountDto.Id,
            CreationDate = DateTime.Now,
            Money = accountDto.Money,
            IsBlocked = accountDto.IsBlocked,
            UserId = accountDto.UserId
        };
        
        // Verificar la cuenta
        if (account == null)
        {
            return null;
        }
        
        await _unitOfWork.AccountRepo.Insert(account);
        await _unitOfWork.SaveChangesAsync();

        return account;
    }

    public async Task<Account> Deposit(int id, int amount, string userId)
    { 
        // Endpoint -> Realizar depósito
        var account = await _unitOfWork.AccountRepo.GetById(id);
        var user = await _unitOfWork.UserRepo.GetById(id);
        
        // Verificar la cuenta
        if (account == null)
        {
            return null;
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

        return account;
    }

    public async Task<Account> Transfer(int id, int toAccount, int amount, string userId)
    {
        // Endpoint -> Realizar transferencia
        var account = await _unitOfWork.AccountRepo.GetById(id);
        var transferId = await _unitOfWork.AccountRepo.GetById(toAccount);
        var user = await _unitOfWork.UserRepo.GetById(id);
        
        // Verificar ambas cuentas
        if (account == null || transferId == null)
        {
            return null;
        }
        // Verificar que la cuenta origen tenga saldo suficiente
        if (account.Money < amount)
        {
            return null;
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
        return account;
    }
    
    public async Task<Account> Update(int id, AccountDTO accountDto)
    {
        // Se obtiene la cuenta
        var account = await _unitOfWork.AccountRepo.GetById(id);

        if (account == null)
        {
            return null;
        }
        
        // Se modifica la cuenta
        account.CreationDate = accountDto.CreationDate;
        account.Money = accountDto.Money;
        account.IsBlocked = accountDto.IsBlocked;
        
        await _unitOfWork.AccountRepo.Update(account);
        await _unitOfWork.SaveChangesAsync();
        return account;
    }

    public async Task<bool> Delete(int id)
    {
        // Se obtiene la cuenta
        var account = await _unitOfWork.AccountRepo.GetById(id);
        
        if (account == null)
        {
            return false;
        }
        
        await _unitOfWork.AccountRepo.Delete(id);
        await _unitOfWork.SaveChangesAsync();
        
        // En caso de realizarse con exito se devuelve un booleano True
        return true;
    }    
}