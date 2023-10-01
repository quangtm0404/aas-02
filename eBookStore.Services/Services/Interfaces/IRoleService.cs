using eBookStore.Services.ViewModels.RoleViewModels;

namespace eBookStore.Services.Services.Interfaces;
public interface IRoleService 
{
    Task<IEnumerable<RoleViewModel>> GetRoles();
    Task<RoleViewModel> GetRoleById(Guid id);
    Task<RoleViewModel> CreateRole(RoleCreateModel role);

}