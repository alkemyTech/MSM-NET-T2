using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace VirtualWallet.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese su Nombre.")]
        public string First_name { get; set; }

        [Required(ErrorMessage = "Ingrese su Apellido.")]
        public string Last_name { get; set; }

        [Required(ErrorMessage = "Ingrese su Email.")]
        [EmailAddress(ErrorMessage = "El Email no tiene un formato válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ingrese su Contraseña.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ingrese sus puntos.")]
        [Range(0, 1000000, ErrorMessage = "el valor de los puntos debe ser de 0 a 1.000.000.")]
        public int Points { get; set; }


        public int Role_Id { get; set; }
        [ForeignKey("Role_Id")]
        public virtual Role Role { get; set; }


        public virtual ICollection<FixedTermDeposit> FixedTermDeposits { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
