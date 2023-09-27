namespace eBookStore.Services.ViewModels.UserViewModels;
public class UserCreateModel
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string MiddleName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public Guid RoleId { get; set; }
    public Guid PublisherId { get; set; }
    public DateTime HireDate { get; set; } = DateTime.UtcNow;
}