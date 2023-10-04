using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eBookStore.Client.Controllers;
public class ProfileController : Controller
{
    private readonly IUserService _userService;
    public ProfileController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Profile(Guid id)
    {
        var result = await _userService.GetUserById(id);
        if (result is not null)
        {
            return View(new UserUpdateModel
            {
                FirstName = result.FirstName,
                Id = result.Id,
                LastName = result.LastName,
                MiddleName = result.MiddleName,
                Password = result.Password,
                Source = result.Source,
                Email = result.Email,
                PublisherId = result.PublisherId,
                RoleId = result.RoleId
            });
        }
        else
        {
            TempData["error"] = $"Not found Users with Id : {id}";
            return RedirectToAction(nameof(Index), "Home");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Profile(UserUpdateModel model)
    {
        var result = await _userService.UpdateUserAsync(model);
        if (result)
        {
            TempData["success"] = $"Update your profile successfully!";
            return RedirectToAction(nameof(Profile), new { id = model.Id });
        }
        else
        {
            TempData["error"] = "Update Your profile failed!";
            return View(model);
        }

    }
}