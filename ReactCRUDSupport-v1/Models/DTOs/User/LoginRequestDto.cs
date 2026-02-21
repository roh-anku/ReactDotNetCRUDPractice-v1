using System.ComponentModel.DataAnnotations;

namespace ReactCRUDSupport_v1.Models.DTOs.User
{
    public class LoginRequestDto
    {
        [Required]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
