using eBookStore.Services.ViewModels.BookAuthorModels;

namespace eBookStore.Services.Services.Interfaces;
public interface IBookAuthorService
{
    Task<IEnumerable<BookAuthorViewModel>> GetAllAsync();
    Task<BookAuthorViewModel> GetBookAuthorByIdAsync(Guid id);
    Task<BookAuthorViewModel> CreateBookAuthorAsync(BookAuthorCreateModel model);
    Task<BookAuthorViewModel> UpdateBookAuthorAsync(BookAuthorUpdateModel model);
    Task<bool> DeleteBookAuthorAsync(Guid id);
}