using System.ComponentModel.DataAnnotations;

namespace Telintec_RH.Models
{
    
        public class AccountViewModel
        {
            [Required]
            public string userName { get; set; } = string.Empty;

            [Required]
            [DataType(DataType.Password)]
            [MinLength(8)]
            public string password { get; set; } = string.Empty;
        }
}
