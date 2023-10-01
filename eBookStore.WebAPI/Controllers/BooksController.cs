using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.ViewModels.BookModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BooksController : ODataController
{
    private readonly IBookService _bookService;
    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }


    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _bookService.GetAllAsync();
        return Ok(result.AsQueryable());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _bookService.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] BookUpdateModel model)
    {
        var result = await _bookService.UpdateAsync(model);
        return StatusCode(StatusCodes.Status204NoContent, result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BookCreateModel model)
    {
        var result = await _bookService.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _bookService.DeleteAsync(id);
        return NoContent();
    }
}