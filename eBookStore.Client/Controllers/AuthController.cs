using Microsoft.AspNetCore.Mvc;

namespace eBookStore.Client.Controllers;
public class AuthController : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
}