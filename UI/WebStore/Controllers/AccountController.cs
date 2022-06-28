using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<User> _UserManager;
    private readonly SignInManager<User> _SignInManager;
    private readonly ILogger<AccountController> _Logger;

    public AccountController(
        UserManager<User> UserManager,
        SignInManager<User> SignInManager,
        ILogger<AccountController> Logger)
    {
        _UserManager = UserManager;
        _SignInManager = SignInManager;
        _Logger = Logger;
    }

    [AllowAnonymous]
    public IActionResult Register() => View(new RegisterUserViewModel());

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterUserViewModel Model)
    {
        if (!ModelState.IsValid)
            return View(Model);

        var user = new User
        {
            UserName = Model.UserName,
        };

        _Logger.LogTrace("Начала процесса регитсрации нового пользователя{0}", Model.UserName);
        var timer = Stopwatch.StartNew();

        using var logger_scope = _Logger.BeginScope("Регитсрация нового пользователя{0}", Model.UserName);
        

            var creation_result = await _UserManager.CreateAsync(user, Model.Password);
            if (creation_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} зарегистрирован за{1} ms", user, timer.ElapsedMilliseconds);

                await _UserManager.AddToRoleAsync(user, Role.Users);

                _Logger.LogInformation("User {0} added new Role {1} for {2} ms", user, Role.Users, timer.ElapsedMilliseconds);

                await _SignInManager.SignInAsync(user, false);

                _Logger.LogTrace("User {0} Logged in system {1} ms", user, timer.ElapsedMilliseconds);

                return RedirectToAction("Index", "Home");
            }

            foreach (var error in creation_result.Errors)
                ModelState.AddModelError("", error.Description);

            var error_info = string.Join(", ", creation_result.Errors.Select(e => e.Description));
            _Logger.LogWarning("Ошибка при регистрации пользователя {0} ({1} ms):{2}",
                user,
                timer.ElapsedMilliseconds,
                error_info
                );

            return View(Model);
        


    }

    [AllowAnonymous]
    public IActionResult Login(string? ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel Model)
    {
        if (!ModelState.IsValid)
            return View(Model);

        _Logger.LogTrace("Strat logging is system with User {1}", Model.UserName);
        var timer = Stopwatch.StartNew();

        using var logger_scope = _Logger.BeginScope("Logging is system with User {1}", Model.UserName);

        var login_result = await _SignInManager.PasswordSignInAsync(
            Model.UserName,
            Model.Password,
            Model.RememberMe,
            lockoutOnFailure: true);

        if (login_result.Succeeded)
        {
            _Logger.LogInformation("Пользователь {0} успешно вошёл в систему. {1} ms", Model.UserName, timer.ElapsedMilliseconds);

            //return Redirect(Model.ReturnUrl);

            //if (Url.IsLocalUrl(Model.ReturnUrl))
            //    return Redirect(Model.ReturnUrl);
            //return RedirectToAction("Index", "Home");

            return LocalRedirect(Model.ReturnUrl ?? "/");
        }

        ModelState.AddModelError("", "Неверное имя пользователя, или пароль");

        _Logger.LogWarning("Ошибка входа пользователя {0} - неверное имя, или пароль. {1}", Model.UserName, timer.ElapsedMilliseconds);

        return View(Model);
    }

    public async Task<IActionResult> Logout()
    {
        var user_name = User.Identity!.Name;

        await _SignInManager.SignOutAsync();

        _Logger.LogInformation("Пользователь {0} вышел из системы", user_name);

        return RedirectToAction("Index", "Home");
    }

    [AllowAnonymous]
    public IActionResult AccessDenied(string? ReturnUrl)
    {
        ViewBag.ReturnUrl = ReturnUrl!;
        return View();
    }
}
