using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telintec_RH.Models
{
    [Table("Absence", Schema = "TIRH")]
    public class Absence
    {
        [Key]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        [Display(Name = "Start_Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime Date_Dep { get; set; }

        [Display(Name = "End_Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMMM-yyyy}")]
        public DateTime Date_Fin { get; set; }

        [Display(Name = "Reason")]
        [Column(TypeName = "varchar(250)")]
        public string? reason { get; set; } = string.Empty;

        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee? Employee { get; set; }
    }
}
