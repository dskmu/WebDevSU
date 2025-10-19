using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WEBKA.Models;

namespace WEBKA.Controllers
{
    public class ContactController : Controller
    {
        private readonly IValidator<ContactForm> _validator;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IValidator<ContactForm> validator, ILogger<ContactController> logger)
        {
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() => View(new ContactForm());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactForm form)
        {
            var result = await _validator.ValidateAsync(form);
            if (!result.IsValid)
            {
                foreach (var e in result.Errors)
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                return View(form);
            }

            _logger.LogInformation("Contact form: Name | LastName | PhoneNumber",
                form.name, form.lastName, form.phone);

            TempData["Result"] = "Ваше сообщение принято!";
            return RedirectToAction(nameof(Index));
        }
    }
}
