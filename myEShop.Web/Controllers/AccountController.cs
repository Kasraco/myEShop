using System.Runtime.CompilerServices;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myEShop.Web.Models;
using myEShop.Web.Models.ViewModels;

namespace myEShop.Web;

public class AccountController : Controller
{

    private readonly myEShopContext _context;
    public AccountController(myEShopContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string? ReturnUrl = null)
    {
        ViewBag.ReturnUrl = ReturnUrl;
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model, string? ReturnUrl = null)
    {
        if (!ModelState.IsValid)
            return View(model);

        var user = GetUser(model.Email.ToLower(), model.Password);
        if (user == null)
        {
            ModelState.AddModelError("Email", "The information entered is not correct");
            return View(model);
        }

        var role = user.IsAdmin ? "Admin" : "User";
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
            new Claim(ClaimTypes.Name,user.Email),
            new Claim(ClaimTypes.Role,role)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsprincipal = new ClaimsPrincipal(identity);

        var properties = new AuthenticationProperties
        {
            IsPersistent = model.RememberMe
        };
        HttpContext.SignInAsync(claimsprincipal, properties);

        if (!string.IsNullOrEmpty(ReturnUrl))
            if (Url.IsLocalUrl(ReturnUrl))
                return Redirect(ReturnUrl);
            else
                return Redirect("/");
        else
            return Redirect("/");
    }

    [HttpGet]

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        if (IsExistEmail(model.Email.ToLower()))
        {

            ModelState.AddModelError("Email", "The entered email is already registered");
            return View(model);
        }

        User user = new User
        {
            Email = model.Email.ToLower(),
            Password = model.Password,
            RegisterDate = DateTime.Now,
            IsAdmin = false
        };
        AddUser(user);

        return View("SuccessRegister", model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/Account/Login");
    }

    [HttpGet]
    public IActionResult AccessDenied(string returnUrl)
    {

        return View();
    }




    private bool IsExistEmail(string email)
    {
        return _context.Users.Any(x => x.Email == email);
    }


    private void AddUser(User user)
    {
        _context.Add(user);
        _context.SaveChanges();

    }

    private User GetUser(string email, string password)
    {
        var em = email.ToLower();
        return _context.Users.SingleOrDefault(x => x.Email == em && x.Password == password);
    }
}
