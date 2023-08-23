using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telintec_RH.Models
{
    [Table("Employee", Schema = "TIRH")]
    public class Employee
    {
        [Key]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        [Required]
        [Display(Name = "full_name")]
        [Column(TypeName = "varchar(100)")]
        public string fulleName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "CIN")]
        [Column(TypeName = "varchar(250)")]
        public string cin { get; set; } = string.Empty;

        [Display(Name = "Image")]
        [Column(TypeName = "varchar(250)")]
        public string? image { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tel")]
        [Column(TypeName = "varchar(100)")]
        public string Tel { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Statu")]
        [Column(TypeName = "varchar(250)")]
        public string statu { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Salary")]
        public double salary { get; set; }

        [Required]
        [Display(Name = "Poste")]
        [Column(TypeName = "varchar(100)")]
        public string poste { get; set; } = string.Empty;

        public int DepartementID { get; set; }
        [ForeignKey("DepartementID")]
        public Departement? Departement { get; set; }
    }
}
