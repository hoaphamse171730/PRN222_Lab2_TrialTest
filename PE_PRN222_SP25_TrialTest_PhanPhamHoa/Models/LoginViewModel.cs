using System.ComponentModel.DataAnnotations;

namespace PE_PRN222_SP25_TrialTest_PhanPhamHoa.Models
{

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}


