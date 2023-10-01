namespace eBookStore.Services.ViewModels.BookModels;
public class BookUpdateModel : BookCreateModel
{
    public Guid Id { get; set; } = default!;
}