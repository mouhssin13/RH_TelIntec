using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telintec_RH.Models
{
    [Table("Departement", Schema = "TIRH")]
    public class Departement
    {
        [Key]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        [Required]
        [Display(Name = "Nom_Departement")]
        [Column(TypeName = "varchar(100)")]
        public string nom { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Chef_Departement")]
        [Column(TypeName = "varchar(250)")]
        public string chef { get; set; } = string.Empty;
    }
}
