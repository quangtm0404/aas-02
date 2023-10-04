using eBookStore.Services.ViewModels.RoleViewModels;

namespace eBookStore.Services.ViewModels.UserViewModels;
public class UserViewModel
{
    public Guid Id {get; set;} 
    public string Email { get; set; } = default!;
	public string Password { get; set; } = default!;
	public string Source { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public Guid RoleId { get; set; } = default!;
	public RoleViewModel Role { get; set; } = default!;
	public Guid PublisherId { get; set; } = default!;
	public DateTime HireDate { get; set; } 
}