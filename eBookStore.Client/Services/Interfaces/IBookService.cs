using eBookStore.Services.ViewModels.BookAuthorModels;
using eBookStore.Services.ViewModels.BookModels;

namespace eBookStore.Client.Services.Interfaces;
public interface IBookService
{
    Task<IEnumerable<BookViewModel>?> GetAllAsync(string query="");
    Task<BookViewModel?> CreateAsync(BookCreateModel model);
    Task<bool> UpdateAsync(BookUpdateModel model);
    Task<bool> DeleteAsync(Guid id);
    Task<BookViewModel?> GetByIdAsync(Guid id); 
    Task<bool> AddBookAuthor(BookAuthorCreateModel model);
}