using System;
using System.ComponentModel.DataAnnotations;

namespace Loymax
{
    public class User
    {
        [Required][RegularExpression(@"[a-zA-ZА-яА-Я]",
         ErrorMessage = "Characters are not allowed.")]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }
        public string MiddleName { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-ZА-яА-Я]",
         ErrorMessage = "Characters are not allowed.")]
        public string LastName { get; set; }
    }
}
 