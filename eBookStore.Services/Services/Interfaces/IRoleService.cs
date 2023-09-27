using eBookStore.Services.ViewModels.RoleViewModels;

namespace eBookStore.Services.Services.Interfaces;
public interface IRoleService 
{
    Task<IEnumerable<RoleViewModel>> GetRole();
    Task<RoleViewModel> GetRoleById(Guid id);
    Task<RoleViewModel> CreateRole(RoleCreateModel role);

}