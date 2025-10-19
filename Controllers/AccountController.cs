using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEBKA.Models;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<User> _users;
    private readonly SignInManager<User> _signIn;

    public AccountController(UserManager<User> users, SignInManager<User> signIn)
    {
        _users = users;
        _signIn = signIn;
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterVm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var user = new User
        {
            UserName = vm.Email,
            Email = vm.Email,
            FirstName = vm.FirstName,
            LastName = vm.LastName
        };

        var create = await _users.CreateAsync(user, vm.Password);
        if (!create.Succeeded)
        {
            foreach (var e in create.Errors)
                ModelState.AddModelError(string.Empty, e.Description);
            return View(vm);
        }

        await _users.AddClaimsAsync(user, new[]
        {
            new System.Security.Claims.Claim("full_name", user.FullName),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.GivenName, user.FirstName ?? ""),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Surname,   user.LastName ?? "")
        });

        await _signIn.SignInAsync(user, isPersistent: false);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null) =>
        View(new LoginVm { ReturnUrl = returnUrl });

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var result = await _signIn.PasswordSignInAsync(
            userName: vm.Email,
            password: vm.Password,
            isPersistent: vm.RememberMe,
            lockoutOnFailure: false);

        if (result.Succeeded)
            return !string.IsNullOrWhiteSpace(vm.ReturnUrl)
                ? LocalRedirect(vm.ReturnUrl!)
                : RedirectToAction("Index", "Home");

        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(vm);
    }

    [Authorize]
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Denied() => View();
}

