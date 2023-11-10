using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace VirtualWallet.Models
{
    public class FixedTermDeposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        //public User User { get; set; }

        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal amount { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime closingDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal nominalRate { get; set; }

        [MaxLength(255)]
        public string state { get; set; }
    }
}
