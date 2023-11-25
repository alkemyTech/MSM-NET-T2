using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using VirtualWallet.DataAccess;
using VirtualWallet.Models;
using VirtualWallet.Models.DTO;
using VirtualWallet.Repository;
using VirtualWallet.Repository.Interfaces;
using VirtualWallet.Services.Interfaces;


namespace VirtualWallet.Services
{
    public class FixedTermService : IFixedTermService
    {
        private readonly UnitOfWork _unitOfWork;
        public FixedTermService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //ADMIN
        public async Task<Object> GetAll(int pageNumber, int pageSize)
        {
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetAll();
            //var filteredFixedTerms = fixedTerms.Where(t => t.UserId.ToString() == userId).OrderBy(t => t.CreationDate); //Filtro los plazos fijos realizados por el usuario logueado

            var pagedFixedTerm = fixedTerms
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            if (fixedTerms == null)
            {
                return null;
            }

            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;

            var nextPage = pageNumber < (int)Math.Ceiling((double)pagedFixedTerm.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                FixedTerm = pagedFixedTerm,
                prevPage = prevPage,
                nextPage = nextPage
            };

            return result;
        }
        //REGULAR
        public async Task<Object> GetAllMyFixedTerms(int pageNumber, int pageSize, string userId)
        {
            var fixedTerms = await _unitOfWork.FixedTermRepo.GetAll();
            var filteredFixedTerms = fixedTerms.Where(t => t.UserId.ToString() == userId).OrderBy(t => t.CreationDate); //Filtro los plazos fijos realizados por el usuario logueado

            var pagedFixedTerm = filteredFixedTerms
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

            if (filteredFixedTerms == null)
            {
                return null;
            }

            var prevPage = pageNumber > 1 ? "Get?pageNumber=" + (pageNumber - 1) + "&pageSize=" + pageSize : null;

            var nextPage = pageNumber < (int)Math.Ceiling((double)pagedFixedTerm.Count() / pageSize) ? "Get?pageNumber=" + (pageNumber + 1) + "&pageSize=" + pageSize : null;

            var result = new
            {
                FixedTerm = pagedFixedTerm,
                prevPage = prevPage,
                nextPage = nextPage
            };

            return result;
        }

        //REGULAR
        public async Task<FixedTermDeposit> GetMyFixedTermById(int id, string userId)
        {
            var allFixedTerms = await _unitOfWork.FixedTermRepo.GetAll();
            var fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);
            var filteredFixedTerms = allFixedTerms.Where(t => t.UserId.ToString() == userId);
            var myfixedTerm = filteredFixedTerms.Where(t => t.Id == id);

            //De no existir el plazo fijo hecho por el usuario se devuelve un null
            if (myfixedTerm == null)
            {
                return null;
            }

            return fixedTerm;
        }
        //ADMIN
        public async Task<FixedTermDeposit> GetById(int id)
        {
            // Se obtiene el plazo fijo
            var fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);

            
            if (fixedTerm == null)
            {
                return null;
            }

            return fixedTerm;
        }

        //ADMIN//
        public async Task<FixedTermDeposit> Post(FixedTermDepositDTO fixedTermDepositDTO)
        { 

            var fixedTerm = new FixedTermDeposit
            {
                Id = fixedTermDepositDTO.Id,
                UserId = fixedTermDepositDTO.UserId,
                AccountId = fixedTermDepositDTO.AccountId,
                Amount = fixedTermDepositDTO.Amount,
                CreationDate = DateTime.Now,
                ClosingDate = fixedTermDepositDTO.ClosingDate,
                NominalRate = fixedTermDepositDTO.NominalRate,
                State = fixedTermDepositDTO.State,
            };

            // Verificar
            if (fixedTerm == null)
            {
                return null;
            }

            await _unitOfWork.FixedTermRepo.Insert(fixedTerm);
            await _unitOfWork.SaveChangesAsync();

            return fixedTerm;
        }

        //REGULAR
        public async Task<FixedTermDeposit> InsertMyNewFixedTerm(FixedTermDepositDTO fixedTermDepositDTO, string userId)
        {
            var userIdValue = int.Parse(userId);

            var fixedTerm = new FixedTermDeposit
            {
                Id = fixedTermDepositDTO.Id,
                UserId = userIdValue,
                AccountId = fixedTermDepositDTO.AccountId,
                Amount = fixedTermDepositDTO.Amount,
                CreationDate = DateTime.Now,
                ClosingDate = fixedTermDepositDTO.ClosingDate,
                NominalRate = fixedTermDepositDTO.NominalRate,
                State = fixedTermDepositDTO.State,
            };

            // Verificar
            if (fixedTerm == null)
            {
                return null;
            }

            await _unitOfWork.FixedTermRepo.Insert(fixedTerm);
            await _unitOfWork.SaveChangesAsync();

            return fixedTerm;
        }


        //ADMIN
        public async Task<FixedTermDeposit> Update(int id, FixedTermDepositDTO fixedTermDepositDTO)
        {
            // Se obtiene el plazo fijo
            var fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);

            if (fixedTerm == null)
            {
                return null;
            }

            // Se modifica el plazo fijo

           // fixedTerm.Id = fixedTermDepositDTO.Id;//
            fixedTerm.UserId = fixedTermDepositDTO.UserId;
            fixedTerm.AccountId = fixedTermDepositDTO.AccountId;
            fixedTerm.Amount = fixedTermDepositDTO.Amount;
            fixedTerm.CreationDate = DateTime.Now;
            fixedTerm.ClosingDate = fixedTermDepositDTO.ClosingDate;
            fixedTerm.NominalRate = fixedTermDepositDTO.NominalRate;
            fixedTerm.State = fixedTermDepositDTO.State;


            await _unitOfWork.FixedTermRepo.Update(fixedTerm);
            await _unitOfWork.SaveChangesAsync();
            return fixedTerm;
        }
        //REGULAR//
        public async Task<FixedTermDeposit> UpdateMyFixedTerm(int id, FixedTermDepositDTO fixedTermDepositDTO, string userId)
        {
            // Se obtiene el plazo fijo y se corrobora si pertenece al usuario
            var userIdValue = int.Parse(userId);
            var allFixedTerms = await _unitOfWork.FixedTermRepo.GetAll();
            var fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);
            var filteredFixedTerms = allFixedTerms.Where(t => t.UserId.ToString() == userId);
            var myfixedTerm = filteredFixedTerms.Where(t => t.Id == id);

            if (myfixedTerm == null)
            {
                return null;
            }

            // Se modifica el plazo fijo

            //  fixedTerm.Id = fixedTermDepositDTO.Id;//
            fixedTerm.UserId = userIdValue;
            fixedTerm.AccountId = fixedTermDepositDTO.AccountId;
            fixedTerm.Amount = fixedTermDepositDTO.Amount;
            fixedTerm.CreationDate = DateTime.Now;
            fixedTerm.ClosingDate = fixedTermDepositDTO.ClosingDate;
            fixedTerm.NominalRate = fixedTermDepositDTO.NominalRate;
            fixedTerm.State = fixedTermDepositDTO.State;

            await _unitOfWork.FixedTermRepo.Update(fixedTerm);
            await _unitOfWork.SaveChangesAsync();
            return fixedTerm;
        }


        //ADMIN//
        public async Task<bool> Delete(int id)
        {
            // Se obtiene el plazo fijo
            var fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);

            if (fixedTerm == null)
            {
                return false;
            }

            await _unitOfWork.FixedTermRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            // En caso de realizarse con exito se devuelve un booleano True
            return true;
        }
        //REGULAR//
        public async Task<bool> DeleteMyFixedTerm(int id, string userId)
        {
            // Se obtiene el plazo fijo
            var allFixedTerms = await _unitOfWork.FixedTermRepo.GetAll();
            var fixedTerm = await _unitOfWork.FixedTermRepo.GetById(id);
            var filteredFixedTerms = allFixedTerms.Where(t => t.UserId.ToString() == userId);
            var myfixedTerm = filteredFixedTerms.Where(t => t.Id == id);

            if (myfixedTerm == null)
            {
                return false;
            }
            await _unitOfWork.FixedTermRepo.Delete(id);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
