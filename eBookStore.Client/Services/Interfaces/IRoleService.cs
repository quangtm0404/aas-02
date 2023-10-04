using eBookStore.Services.ViewModels.RoleViewModels;

namespace eBookStore.Client.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleViewModel>?> GetAllRoleAsync();

    }
}
