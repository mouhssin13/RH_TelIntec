using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telintec_RH.Models
{
    [Table("Account", Schema = "TIRH")]
    public class Account
        
    {
        [Key]
        [Display(Name = "ID")]
        public int? ID { get; set; }

        [Required]
        [Display(Name = "first_name")]
        [Column(TypeName = "varchar(100)")]
        public string firstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "last_name")]
        [Column(TypeName = "varchar(100)")]
        public string lastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "user_name")]
        [Column(TypeName = "varchar(250)")]
        public string userName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Tel")]
        [Column(TypeName = "varchar(100)")]
        public string Tel { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Password")]
        [Column(TypeName = "varchar(250)")]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string password { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Role")]
        [Column(TypeName = "varchar(100)")]
        public string role { get; set; } = string.Empty;

    }
}
