using FluentValidation;
namespace WEBKA.Models
{
    public class ContactFormValidator : AbstractValidator<ContactForm>
    {

          public ContactFormValidator()
          {
            RuleFor(x => x.name)
                .NotEmpty()
                .WithMessage("Поле 'Имя' не должно быть пустым");


            RuleFor(x => x.lastName)
                .NotEmpty()
                .WithMessage("Поле 'Фамилия' не должно быть пустым");


            RuleFor(x => x.phone)
                .NotEmpty()
                .WithMessage("Поле 'Телефон' не должно быть пустым");

            RuleFor(x => x.message)
                .NotEmpty()
                .WithMessage("Поле 'Сообщение' не должно быть пустым");
          }

    }
}

