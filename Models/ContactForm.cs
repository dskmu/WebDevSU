using System.ComponentModel.DataAnnotations;

namespace WEBKA.Models
{
    public class ContactForm
    {
        [Required(ErrorMessage = "Поле First Name обязательно к заполнению")]
        [Display(Name = "First Name")]
        public string name { get; set; }

        [Required(ErrorMessage = "Поле Last Name обязательно к заполнению")]
        [Display(Name = "Last Name")]
        public string lastName { get; set; }

        [Required(ErrorMessage = "Телефон обязателен к заполнению")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+?[0-9\s\-\(\)]{7,20}$", ErrorMessage = "Введите корректный номер телефона")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Текст сообщения обязателен к заполнению")]
        [StringLength(1000, ErrorMessage = "Текст сообщения слишком большой (максимум 1000 символов)")]
        [Display(Name = "Message")]
        public string message { get; set; }


        [Required(ErrorMessage = "Поле Email обязательно к заполнению")]
        [EmailAddress(ErrorMessage = "Поле Email обязателен к заполнению")]
        public string email { get; set; }


    }
}
