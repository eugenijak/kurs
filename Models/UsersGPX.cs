using System.ComponentModel.DataAnnotations;

namespace Kurs.Models
{
    public class UsersGPX
    {
        [Key]
        public int Id { get; set; }

        public int Id_user { get; set; }

        [Required(ErrorMessage = "Введите название")]
        public string name { get; set; }

        //[Required(ErrorMessage = "Выберите файл")]
        //public string filepath { get; set; }
        [Required(ErrorMessage = "Введите Цену")]
        public string price { get; set; }
    }
}
