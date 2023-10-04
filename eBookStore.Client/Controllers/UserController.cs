using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eBookStore.Client.Controllers;
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IPublisherService _publisherService;
    public UserController(IUserService userService, IPublisherService publisherService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
        _publisherService = publisherService;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _userService.GetAllUserAsync();
        if (result is not null)
        {
            if (result.Count() > 0)
            {
                return View(result);
            }
        }

        TempData["error"] = "Not have any users";
        return View(new List<UserViewModel>());
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var publisherList = await _publisherService.GetAllAsync();
        var roleList = await _roleService.GetAllRoleAsync();
        if (roleList != null && publisherList != null)
        {
            ViewData["RoleId"] = new SelectList(roleList, "Id", "RoleName");
            ViewData["PublisherId"] = new SelectList(publisherList, "Id", "Name");
            return View(new UserCreateModel());
        }
        else
        {
            TempData["error"] = "PublisherList or RoleList is null";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateModel model)
    {
        var result = await _userService.CreateUser(model);
        if(result is not null) 
        {
            TempData["success"] = "Create User Successfully!";
            return RedirectToAction(nameof(Index));
        } else 
        {
            TempData["error"] = "Create User Failed!";
            return View(model);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _userService.GetUserById(id);
        System.Console.WriteLine($"--> User: {result!.Id}");
        if (result is not null)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = $"Not found Users with Id : {id}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Details(UserViewModel model)
    {
        var result = await _userService.UpdateUserAsync(new UserUpdateModel
        {
            Id = model.Id,
            RoleId = model.RoleId,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            Password = model.Password,
            Source = model.Source,
            PublisherId = model.PublisherId
        });
        if(result)
        {
            TempData["success"] = "Update User Successfully!";
            return RedirectToAction("Index");
        } else {
            TempData["error"] = "Update User Failed!";
            return View(model);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _userService.GetUserById(id);
        if (result is not null)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = $"Not found Users with Id : {id}";
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(UserViewModel model)
    {
        var result = await _userService.DeleteUserAsync(model.Id);
        if (result)
        {
            TempData["success"] = $"Delete User: {model.Id} successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = $"Delete User: {model.Id} failed!";
            return View(model);
        }
    }


}