using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Telintec_RH.Models
{

    [Table("Holiday", Schema = "TIRH")]
    public class Holiday
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

        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee? Employee { get; set; }
    }
}
