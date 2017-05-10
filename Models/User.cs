using System.ComponentModel.DataAnnotations;

namespace Kurs.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string name { get; set; }

        [Required(ErrorMessage = "Введите email")]
        [RegularExpression("^[-\\w.]+@([A-z0-9][-A-z0-9]+\\.)+[A-z]{2,4}$", ErrorMessage = "Неверный email")]
        public string email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(8,ErrorMessage = "Короткий пароль")]
        public string password { get; set; }
    }
}
