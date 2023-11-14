using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualWallet.Models
{
    public class Catalogue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(255)]
        public string ProductDescription { get; set; }

        [MaxLength(255)]
        public string Image { get; set; }

        public int Points { get; set; }
    }
}
