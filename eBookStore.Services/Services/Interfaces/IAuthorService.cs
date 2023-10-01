using eBookStore.Services.ViewModels.AuthorViewModels;

namespace eBookStore.Services.Services.Interfaces;
public interface IAuthorService
{
    Task<IEnumerable<AuthorViewModel>> GetAllAsync();
    Task<AuthorViewModel> GetById(Guid id);
    Task<AuthorViewModel> CreateAuthor(AuthorCreateModel model);
    Task<AuthorViewModel> UpdateAuthor(AuthorUpdateModel model);
    Task<bool> DeleteAuthor(Guid id);
}