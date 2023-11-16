using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public decimal Money { get; set; } 
        public bool IsBlocked { get; set; }
        public int UserId { get; set; }
    }
}
