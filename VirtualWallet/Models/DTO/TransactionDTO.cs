using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualWallet.Models.DTO
{
    public class TransactionDTO
    {
        public int transactionId { get; set; }
        public decimal Amount { get; set; }

        public string Concept { get; set; }

        public DateTime Date { get; set; }

        public string Type { get; set; } 

        public int AccountId { get; set; }

        public int UserId { get; set; } 

        public int? ToAccountId { get; set; } 

    }
}
