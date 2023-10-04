using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eBookStore.Client.Controllers;
public class AuthController : Controller
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        TempData["success"] = "Logout Successfully!";
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginModel model)
    {
        var result = await _authService.LoginAsync(model);
        if (result is not null)
        {
            LoginResponseModel loginResponse = (LoginResponseModel)result;
            await SignInUser(loginResponse);
            return RedirectToAction("Index", "Home");
        }
        else
        {
            TempData["error"] = "Wrong username or password!";
            return View(model);
        }

    }

    private async Task SignInUser(LoginResponseModel model)
    {
        // Get Token, Read Token, And assign claims into identity
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(model.Token);
        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Email)!.Value));
        identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(x => x.Type == "role")!.Value));
        System.Console.WriteLine("--> Role: " + identity.Claims.First(x => x.Type == ClaimTypes.Role));
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}