using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.PublisherModels;
using Microsoft.AspNetCore.Mvc;

namespace eBookStore.Client.Controllers;
public class PublisherController : Controller
{
    private readonly IPublisherService _publisherService;
    public PublisherController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _publisherService.GetAllAsync();
        if (result is not null && result.Count() > 0)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = "Publisher List Is Empty! Please Create Publisher First!";
            return RedirectToAction(nameof(Create));
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new PublisherCreateModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(PublisherCreateModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var result = await _publisherService.CreateAsync(model);
        if (result is not null)
        {
            TempData["success"] = "Create Successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = "Create Failed!";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _publisherService.GetByIdAsync(id);
        if (result is not null)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = $"Not Found Publisher With Id: {id}";
            return RedirectToAction(nameof(Index));
        }

    }

    [HttpPost]
    public async Task<IActionResult> Details(PublisherViewModel model)
    {
        var result = await _publisherService.UpdateAsync(new PublisherUpdateModel
        {
            Id = model.Id,
            City = model.City,
            Country = model.Country,
            Name = model.Name,
            State = model.State
        });
        if (result)
        {
            TempData["success"] = $"Update Information for Publisher with Id: {model.Id} successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = $"Update Information for Publisher with Id: {model.Id} Failed!";
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _publisherService.GetByIdAsync(id);
        if (result is not null)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = $"Not Found Publisher With Id: {id}";
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    public async Task<IActionResult> Search(string search)
    {
        var result = await _publisherService.GetAllAsync(search);
        if (result is not null && result.Count() > 0)
            return View(nameof(Index), result);
        else
        {
            TempData["error"] = $"Not found any Publisher with that keyword!";
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(PublisherViewModel model)
    {
        var result = await _publisherService.DeleteAsync(model.Id);
        if (result)
        {
            TempData["success"] = $"Delete Publisher Successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = $"Delete Publisher Failed!";
            return RedirectToAction(nameof(Delete), new { id = model.Id });
        }

    }
}