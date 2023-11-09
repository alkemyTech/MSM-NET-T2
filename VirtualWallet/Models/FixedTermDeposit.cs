using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace VirtualWallet.Models
{
    public class FixedTermDeposit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [ForeignKey("User")]
        public int userId { get; set; }
        public User User { get; set; }

        [ForeignKey("Account")]
        public int accountId { get; set; }
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
