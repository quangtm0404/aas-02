using eBookStore.Services.ViewModels.BookAuthorModels;

namespace eBookStore.Services.ViewModels.BookModels;
public class BookUpdateModel : BookCreateModel
{
    public Guid Id { get; set; } = default!;
    public ICollection<BookAuthorCreateModel> BookAuthors { get; set; } = default!;
}