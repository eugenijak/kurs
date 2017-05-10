using System.ComponentModel.DataAnnotations;

namespace Kurs.Models
{
    public class Password
    { 
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(8, ErrorMessage = "Короткий пароль")]
        public string password { get; set; }
    }
}
