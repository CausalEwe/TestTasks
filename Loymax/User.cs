using System;
using System.ComponentModel.DataAnnotations;

namespace Loymax
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Укажите имя")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я''-'\s]{1,40}$", ErrorMessage = "Имя должно содержать только буквы")]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        [RegularExpression(@"^[a-zA-Zа-яА-Я''-'\s]{1,40}$", ErrorMessage = "Имя должно содержать только буквы")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "Укажите фамилию")]
        [RegularExpression(@"^[a-zA-Zа-яА-Я''-'\s]{1,40}$", ErrorMessage = "Фамилия должна содержать только буквы")]
        public string LastName { get; set; }
        [ConcurrencyCheck]
        public decimal Balance { get; set; }
    }
}
 