using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace eBookStore.WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ODataController
{
    public IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (await _userService.DeleteAsync(id))
            return NoContent();
        else return BadRequest();
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateModel model)
    {
        var result = await _userService.CreateAsync(model);
        if (result is not null)
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        else return BadRequest($"Create Failed!");
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = (await _userService.GetAllUsersAsync()).FirstOrDefault(x => x.Id == id);
        if (result is not null) return Ok(result);
        else return NotFound($"Not found user with Id: {id}");
    }

    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok((await _userService.GetAllUsersAsync()).AsQueryable());
    }
    [EnableQuery]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UserUpdateModel model)
    {
        var result = await _userService.UpdateAsync(model);
        if (result is not null)
        {
            return StatusCode(StatusCodes.Status204NoContent, result);
        }
        else return BadRequest("Update failed!");
    }



}