using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.BookAuthorModels;
using eBookStore.Services.ViewModels.BookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace eBookStore.Client.Controllers;
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IPublisherService _publisherService;
    private readonly IAuthorService _authorService;
    private readonly IMemoryCache _memoryCache;
    public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisheService, IMemoryCache memoryCache)
    {
        _bookService = bookService;
        _authorService = authorService;
        _publisherService = publisheService;
        _memoryCache = memoryCache;
    }

    [HttpPost]
    public async Task<IActionResult> Search(string search)
    {
         var result = await _bookService.GetAllAsync(search);
        if(result is not null)
         return View(nameof(Index), result);
        else 
        {
            TempData["error"] = $"Not found any book with that keyword!";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _bookService.GetAllAsync();
        if (result is not null)
            if (result.Count() > 0)
                return View(result);

        TempData["error"] = "Book List Empty! Please Create Book First!";
        return RedirectToAction(nameof(Create));
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {

        var publisherList = await _publisherService.GetAllAsync();

        if (publisherList == null)
        {
            TempData["error"] = "Publisher List or Author List is null!";
            return RedirectToAction("Index", "Home");
        }

        ViewData["PublisherId"] = new SelectList(publisherList, "Id", "Name");

        return View(new BookCreateModel());
    }

    [HttpGet]
    public async Task<IActionResult> AddAuthor(Guid id)
    {
        var authorList = await _authorService.GetAllAsync();
        if (authorList is not null)
        {
            var auList = authorList.Select(au =>
            new
            {
                Id = au.Id,
                Name = au.LastName + " " + au.FirstName
            });
            ViewData["AuthorId"] = new SelectList(auList, "Id", "Name");
            var model = new BookAuthorCreateModel
            {
                BookId = id
            };
            return View(model);
        }
        else
        {
            TempData["error"] = $"Not found Book Author with Id: {id}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAuthor(BookAuthorCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(AddAuthor), new { id = model.BookId });
        }

        var result = await _bookService.AddBookAuthor(model);
        if (result)
        {
            TempData["success"] = $"Add Author to Book successfully!";
            return RedirectToAction(nameof(Details), new { id = model.BookId });
        }
        else
        {
            TempData["error"] = "Add Author Failed! Author has Addded!";
            return RedirectToAction(nameof(AddAuthor), new { id = model.BookId });
        }

    }
    [HttpPost]
    public async Task<IActionResult> Create(BookCreateModel model)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Create));
        }
        var result = await _bookService.CreateAsync(model);
        if (result is not null)
        {
            TempData["success"] = "Create Book Successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = "Create Book Failed!";
            return View(result);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _bookService.GetByIdAsync(id);
        if (result is not null)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = $"Not found Book with Id: {id}";
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    public async Task<IActionResult> Delete(BookViewModel model)
    {
        var result = await _bookService.DeleteAsync(model.Id);
        if (result)
        {
            TempData["success"] = $"Delete Book {model.Id} successfully!";
            return RedirectToAction(nameof(Index));
        }
        else
        {
            TempData["error"] = $"Delete Failed!";
            return RedirectToAction(nameof(Delete), new { id = model.Id });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        var result = await _bookService.GetByIdAsync(id);
        if (result is not null)
        {
            return View(result);
        }
        else
        {
            TempData["error"] = $"Not found Book with Id: {id}";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Details(BookViewModel model)
    {
        var result = await _bookService.UpdateAsync(new BookUpdateModel
        {
            Id = model.Id,
            Advance = model.Advance,
            BookAuthors = model.BookAuthors,
            Notes = model.Notes,
            Price = model.Price,
            Royalty = model.Royalty,
            PublishedDate = model.PublishedDate,
            Title = model.Title,
            PublisherId = model.PublisherId,
            Type = model.Type
        });
        if (result)
        {
            TempData["success"] = $"Update Book Successfully!";
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }
        else
        {
            TempData["error"] = $"Update Book Failed!";
            return View(model);
        }
    }

}