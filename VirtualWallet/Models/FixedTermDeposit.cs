using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace VirtualWallet.Models
{
    public class FixedTermDeposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [BindNever]
        public int Id { get; set; }

        [BindNever]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ClosingDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal NominalRate { get; set; }

        [MaxLength(255)]
        public string State { get; set; }
    }
}
