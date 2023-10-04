namespace eBookStore.Services.ViewModels.UserViewModels;
public class UserUpdateModel
{
    public Guid Id {get; set;} 
	public string Email {get; set;} = default!;
    public string Password { get; set; } = default!;
	public string Source { get; set; } = default!;
	public string FirstName { get; set; } = default!;
	public string MiddleName { get; set; } = default!;
	public string LastName { get; set; } = default!;
	public Guid RoleId { get; set; } = default!;
	public Guid PublisherId { get; set; } = default!;
}