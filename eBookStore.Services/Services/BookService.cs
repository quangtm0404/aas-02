using AutoMapper;
using eBookStore.Domains.Entities;
using eBookStore.Services.Services.Interfaces;
using eBookStore.Services.ViewModels.BookModels;

namespace eBookStore.Services.Services;
public class BookService : IBookService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public BookService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookViewModel> CreateAsync(BookCreateModel model)
    {
        var book = _mapper.Map<Book>(model);
        if (book is not null)
        {
            await _unitOfWork.BookRepository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BookViewModel>(await _unitOfWork.BookRepository.GetByIdAsync(book.Id, x => x.BookAuthors));
        }
        else throw new AutoMapperMappingException("Unsupported Mapping");
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(id, x => x.BookAuthors);
        if (book != null)
        {
            if (book.BookAuthors.Count() > 0)
            {
                foreach (var item in book.BookAuthors)
                {
                    _unitOfWork.BookAuthorRepository.SoftRemove(item);
                }
            }
            _unitOfWork.BookRepository.SoftRemove(book);
            return await _unitOfWork.SaveChangesAsync();
        }
        else throw new Exception("Unsupported mapping!");
    }

    public async Task<IEnumerable<BookViewModel>> GetAllAsync()
    => _mapper.Map<IEnumerable<BookViewModel>>(await _unitOfWork.BookRepository.GetAllAsync(x => x.BookAuthors));

    public async Task<BookViewModel> GetByIdAsync(Guid id) => _mapper.Map<BookViewModel>(await _unitOfWork.BookRepository.GetByIdAsync(id, x => x.BookAuthors));

    public async Task<BookViewModel> UpdateAsync(BookUpdateModel model)
    {
        var book = await _unitOfWork.BookRepository.GetByIdAsync(model.Id, x => x.BookAuthors);
        if (book != null)
        {
            _mapper.Map(model, book);
            _unitOfWork.BookRepository.Update(book);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<BookViewModel>(await _unitOfWork.BookRepository.GetByIdAsync(book.Id, x => x.BookAuthors));
        }
        else throw new Exception($"Not found Book with Id: {model.Id}");
    }
}