using eBookStore.Client.Services.Interfaces;
using eBookStore.Services.ViewModels.BookModels;
using Microsoft.AspNetCore.Mvc;

namespace eBookStore.Client.Controllers;
public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IPublisherService _publisherService;
    private readonly IAuthorService _authorService;
    public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisheService)
    {
        _bookService = bookService;
        _authorService = authorService;
        _publisherService = publisheService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await _bookService.GetAllAsync();
        if(result is not null) 
            if(result.Count() > 0)
                return View(result);
        
        TempData["error"] = "Book List Empty! Please Create Book First!";
        return RedirectToAction()
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var publisherList = await _publisherService.GetAllAsync();
        var authorList = await _authorService.GetAllAsync();
        ViewData.Add();
        return View(new BookCreateModel());
    }
}