using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.AuthorViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eBookStore.Client.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpPost]
        public async Task<IActionResult> Search(string search)
        {
            var result = await _authorService.GetAllAsync(search);
            if (result is not null)
                return View(nameof(Index), result);
            else
            {
                TempData["error"] = $"Not found any Author with that keyword!";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = (await _authorService.GetAllAsync()) ?? new List<AuthorViewModel>();
            if (result.Count() > 0)
                return View(result);
            else
            {
                TempData["error"] = "Author List is empty! Create First!";
                return RedirectToAction(nameof(Create));
            }




        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AuthorCreateModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuthorCreateModel model)
        {
            var result = await _authorService.CreateAsync(model);
            if (result is not null)
            {
                TempData["success"] = "Create Author successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Create Author Failed!";
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (result is not null)
            {
                return View(result);
            }
            else
            {
                TempData["error"] = $"Not found Author with Id: {id}";
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Details(AuthorViewModel model)
        {
            var result = await _authorService.UpdateAsync(new AuthorUpdateModel
            {
                Address = model.Address,
                City = model.City,
                Email = model.Email,
                FirstName = model.FirstName,
                Id = model.Id,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Zip = model.Zip,
                State = model.State
            });
            if (result)
            {
                TempData["success"] = $"Update Author Successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = $"Update Author Failed!";
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (result is not null)
            {
                return View(result);
            }
            else
            {
                TempData["error"] = $"Not found Author with Id: {id}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AuthorViewModel model)
        {
            var result = await _authorService.DeleteAsync(model.Id);
            if (result)
            {
                TempData["success"] = "Delete Author Successfully!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = "Delete Author Failed!";
                return View(model);
            }
        }
    }
}
