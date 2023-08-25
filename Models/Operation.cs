using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telintec_RH.Models
{
    [Table("Operation", Schema = "TIRH")]
    public class Operation
    {
        [Key]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Contrat")]
        [Column(TypeName = "varchar(250)")]
        public string? contrat { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Montant")]
        public double montant { get; set; }

        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee? Employee { get; set; }
    }
}
