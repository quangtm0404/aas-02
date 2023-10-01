namespace eBookStore.Services.ViewModels.AuthorViewModels;
public class AuthorUpdateModel : AuthorCreateModel
{
    public Guid Id { get; set; } = default;
}