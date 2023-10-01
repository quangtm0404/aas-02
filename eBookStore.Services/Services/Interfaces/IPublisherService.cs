using eBookStore.Services.ViewModels.PublisherModels;

namespace eBookStore.Services.Services.Interfaces;
public interface IPublisherService
{
    Task<IEnumerable<PublisherViewModel>> GetAllAsync();
    Task<PublisherViewModel> GetByIdAsync(Guid id);
    Task<PublisherViewModel> CreateAsync(PublisherCreateModel model);
    Task<PublisherViewModel> UpdateAsync(PublisherUpdateModel model);
    Task<bool> DeleteAsync(Guid id);
}