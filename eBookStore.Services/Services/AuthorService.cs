
using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.ViewModels.AuthorViewModels;

namespace eBookStore.Services.Services;
public class AuthorService : IAuthorService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public AuthorService(IMapper mapper, IUnitOfWork unitOfWork)
    {   
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthorViewModel> CreateAuthor(AuthorCreateModel model)
    {
        var author = _mapper.Map<Author>(model);
        await _unitOfWork.AuthorRepository.AddAsync(author);
        if(await _unitOfWork.SaveChangesAsync())
            return _mapper.Map<AuthorViewModel>(await _unitOfWork.AuthorRepository.GetByIdAsync(author.Id));
        else throw new Exception("Save changes failed!");
    }

    public async Task<bool> DeleteAuthor(Guid id)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(id);
        if(author is not null) 
        {
            _unitOfWork.AuthorRepository.SoftRemove(author);
            if(await _unitOfWork.SaveChangesAsync())
                return true;
            else return false;
        } else throw new Exception($"Not found Author with Id : {id}");
    }

    public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<AuthorViewModel>>(await _unitOfWork.AuthorRepository.GetAllAsync());
    }

    public async Task<AuthorViewModel> GetById(Guid id)
        => _mapper.Map<AuthorViewModel>(await _unitOfWork.AuthorRepository.GetByIdAsync(id));

    public async Task<AuthorViewModel> UpdateAuthor(AuthorUpdateModel model)
    {
        var author = await _unitOfWork.AuthorRepository.GetByIdAsync(model.Id);
        if(author is not null)
        {
            _mapper.Map(model, author);
            _unitOfWork.AuthorRepository.Update(author);
            if(await _unitOfWork.SaveChangesAsync())
                return _mapper.Map<AuthorViewModel>(await _unitOfWork.AuthorRepository.GetByIdAsync(author.Id));
            else throw new Exception($"Save changes failed!");
        } else throw new Exception($"Not found author with Id: {model.Id}");
    }
        
    
}