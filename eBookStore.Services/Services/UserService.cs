using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.Utilities;
using eBookStore.Services.ViewModels.UserViewModels;
using Microsoft.Extensions.Options;

namespace eBookStore.Services.Services;
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtOption _jwtOptions;
    public UserService(IMapper mapper, IUnitOfWork unitOfWork, IOptions<JwtOption> options)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtOptions = options.Value;
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

    public async Task<UserViewModel> LoginAsync(string email, string password)
        => _mapper.Map<UserViewModel>(await _unitOfWork.UserRepository.FindByField(x => x.Email == email && x.Password == password));

    public async Task<UserViewModel> UpdateAsync(UserUpdateModel userUpdateModel)
    {
        var userInDb = await _unitOfWork.UserRepository.GetByIdAsync(userUpdateModel.Id);
        if (userInDb is not null)
        {
            _mapper.Map(userUpdateModel, userInDb);
            _unitOfWork.UserRepository.Update(userInDb);
            return await _unitOfWork.SaveChangesAsync() ? 
                _mapper.Map<UserViewModel>(await _unitOfWork.UserRepository.GetByIdAsync(userInDb.Id))
                : throw new Exception("Save changes failed!");
        }
        else
        {
            throw new Exception($"Not found User with Id {userUpdateModel.Id}");
        }
    }
}
