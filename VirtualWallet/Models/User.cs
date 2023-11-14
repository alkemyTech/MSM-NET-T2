using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualWallet.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Points { get; set; }

       
        public int Role_Id { get; set; }
        [ForeignKey("Role_Id")]
        public virtual Role Role { get; set; }


        public virtual ICollection<FixedTermDeposit> FixedTermDeposits { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
