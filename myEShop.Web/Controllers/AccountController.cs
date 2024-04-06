using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myEShop.Web.Models;
using myEShop.Web.Models.ViewModels;

namespace myEShop.Web;

public class AccountController : Controller
{

    private readonly myEShopContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string? ReturnUrl = null)
    {
        ViewData["ReturnUrl"] = ReturnUrl;
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl = null)
    {
        ViewData["ReturnUrl"] = ReturnUrl;
        if (!ModelState.IsValid)
            return View(model);


        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(ReturnUrl))
                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return Redirect("/");
            else
                return Redirect("/");

        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attemp");
        }

        // var role = user.IsAdmin ? "Admin" : "User";
        // var claims = new List<Claim>
        // {
        //     new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
        //     new Claim(ClaimTypes.Name,user.Email),
        //     new Claim(ClaimTypes.Role,role)
        // };

        // var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        // var claimsprincipal = new ClaimsPrincipal(identity);

        // var properties = new AuthenticationProperties
        // {
        //     IsPersistent = model.RememberMe
        // };
        // HttpContext.SignInAsync(claimsprincipal, properties);

        return View(model);
    }

    [HttpGet]

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        IdentityUser user = new IdentityUser
        {
            Email = model.Email,
            UserName = model.Email
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            return View("SuccessRegister", model);
        }
        else
        {
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        // HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(AccountController.Login), "Account");
    }

    [HttpGet]
    public IActionResult AccessDenied(string returnUrl)
    {
        return View();
    }

}
