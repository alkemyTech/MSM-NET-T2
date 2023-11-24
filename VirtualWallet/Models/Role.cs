using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models;

public class Role
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // ID auto incremental
    public int Id { get; set; }

    [MaxLength(255)]
    [Required(ErrorMessage = "El campo Name es obligatorio.")]
    public string Name { get; set; }

    [MaxLength(255)]
    [Required(ErrorMessage = "El campo Description es obligatorio.")]
    public string Description { get; set; }
}