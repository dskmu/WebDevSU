using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WEBKA.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        public IActionResult Index() => View();
    }
}
