using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VirtualWallet.Models.DTO
{
    public class FixedTermDepositDTO
    {
        [BindNever]
        public int Id { get; set; }

        [BindNever]
        public int UserId { get; set; }

        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ClosingDate { get; set; }

        public decimal NominalRate { get; set; }

        public string State { get; set; }
    }
}
