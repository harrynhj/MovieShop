using System.Diagnostics;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Models;

namespace MovieShop.Controllers;

public class AccountController: Controller
{
    private readonly IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    
    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (_accountService.HasDupEmail(model.Email))
        {
            ModelState.AddModelError("Email", "Email is already taken");
            return View(model);
        }

        if (model.DateOfBirth > DateTime.Now)
        {
            ModelState.AddModelError("DateOfBirth", "Date of birth is invalid");
            return View(model);
        }
        _accountService.RegisterAccount(model);
        TempData["SuccessMessage"] = "Registration successful!";
        return RedirectToAction("Login");
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _accountService.AuthenticateUser(model);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
            new Claim(ClaimTypes.Role, user.role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync("MyCookieAuth");
        return RedirectToAction("Index", "Home");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}