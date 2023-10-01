using eBookStore.Services.ViewModels.BookModels;

namespace eBookStore.Services.Services.Interfaces;
public interface IBookService
{
    Task<IEnumerable<BookViewModel>> GetAllAsync();
    Task<BookViewModel> GetByIdAsync(Guid id);
    Task<BookViewModel> CreateAsync(BookCreateModel model);
    Task<BookViewModel> UpdateAsync(BookUpdateModel model);
    Task<bool> DeleteAsync(Guid id);
}