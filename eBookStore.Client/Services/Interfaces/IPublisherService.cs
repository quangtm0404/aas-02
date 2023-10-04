using eBookStore.Services.ViewModels.PublisherModels;

namespace eBookStore.Client.Services.Interfaces
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherViewModel>?> GetAllAsync(string search="");
        Task<PublisherViewModel?> GetByIdAsync(Guid id);
        Task<PublisherViewModel?> CreateAsync(PublisherCreateModel model);
        Task<bool> UpdateAsync(PublisherUpdateModel model);
        Task<bool> DeleteAsync(Guid id);

    }
}
