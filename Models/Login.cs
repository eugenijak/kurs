using System.ComponentModel.DataAnnotations;

namespace Kurs.Models
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string password { get; set; }

        public string error { get; set; }
    }
}
