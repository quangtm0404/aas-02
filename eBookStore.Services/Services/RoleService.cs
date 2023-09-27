using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.ViewModels.RoleViewModels;

namespace eBookStore.Services.Services;
public class RoleService : IRoleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<RoleViewModel> CreateRole(RoleCreateModel roleModel)
    {
        var role = _mapper.Map<Role>(roleModel);
        await _unitOfWork.RoleRepository.AddAsync(role);
        if(await _unitOfWork.SaveChangesAsync()) 
        {
            return _mapper.Map<RoleViewModel>(await _unitOfWork.RoleRepository.GetByIdAsync(role.Id));
        } else throw new Exception("Save Changes Failed");
    }

    public async Task<IEnumerable<RoleViewModel>> GetRole() 
        => _mapper.Map<IEnumerable<RoleViewModel>>(await _unitOfWork.RoleRepository.GetAllAsync());
   

    public async Task<RoleViewModel> GetRoleById(Guid id)
        => _mapper.Map<RoleViewModel>(await _unitOfWork.RoleRepository.GetByIdAsync(id));
}