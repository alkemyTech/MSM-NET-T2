using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models.DTO
{
    public class AccountDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "El campo Creation Date es obligatorio.")]
        public DateTime CreationDate { get; set; }
        
        [Column(TypeName = "decimal(10, 2)")]
        [Required(ErrorMessage = "El campo Money es obligatorio.")]
        public decimal Money { get; set; } 
        
        [Required(ErrorMessage = "El campo IsBlocked es obligatorio.")]
        public bool IsBlocked { get; set; }
       
        [Required(ErrorMessage = "El campo UserId es obligatorio.")]
        public int UserId { get; set; }
    }
}
