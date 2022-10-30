using System.ComponentModel.DataAnnotations;

namespace BackendSWGAF.Models.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string passsword { get; set; }
    }
}
