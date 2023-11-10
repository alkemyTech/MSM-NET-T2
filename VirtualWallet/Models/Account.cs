using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal Money { get; set; }
    public bool IsBlocked { get; set; }
    
    //[ForeignKey("IdUser")]
    //public virtual User User { get; set; }
}
