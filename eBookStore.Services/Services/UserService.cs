using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.ViewModels.UserViewModels;

namespace eBookStore.Services.Services;
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UserService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserViewModel> CreateAsync(UserCreateModel userCreateModel)
    {
        var user = _mapper.Map<User>(userCreateModel);
        await _unitOfWork.UserRepository.AddAsync(user);
        return await _unitOfWork.SaveChangesAsync() ?
            _mapper.Map<UserViewModel>(await _unitOfWork.UserRepository.GetByIdAsync(user.Id))
                : throw new Exception("Save changes failed!");
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
        if (user is not null)
        {
            _unitOfWork.UserRepository.SoftRemove(user);
            return await _unitOfWork.SaveChangesAsync() ? true : throw new Exception("Save changes failed!");
        }
        else
        {
            throw new Exception($"Not found user with Id: {id}");
        }
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        => _mapper.Map<IEnumerable<UserViewModel>>(await _unitOfWork.UserRepository.GetAllAsync());

    public Task<UserViewModel> LoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
