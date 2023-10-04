using eBookStore.Services.ViewModels.AuthorViewModels;

namespace eBookStore.Client.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>?> GetAllAsync(string search="");
        Task<AuthorViewModel?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(AuthorUpdateModel model);
        Task<AuthorViewModel?> CreateAsync(AuthorCreateModel model);

    }
}
