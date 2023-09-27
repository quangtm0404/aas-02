using eBookStore.Repositories.Data;
using eBookStore.Services;
using eBookStore.Services.Repositories;

namespace eBookStore.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IBookRepository _bookRepo;
    private readonly IBookAuthorRepository _bookAuthorRepo;
    private readonly IUserRepository _userRepo;
    private readonly IRoleRepository _roleRepo;
    private readonly IPublisherRepository _publisherRepo;
    private readonly AppDbContext _dbContext;
    public UnitOfWork(AppDbContext dbContext, IBookAuthorRepository bookAuthorRepo, IAuthorRepository authorRepo
    , IBookRepository bookRepo, IUserRepository userRepo, IRoleRepository roleRepo, IPublisherRepository pubRepo)
    {
        _authorRepository = authorRepo;
        _bookAuthorRepo = bookAuthorRepo;
        _dbContext = dbContext;
        _bookRepo = bookRepo;
        _userRepo = userRepo;
        _roleRepo = roleRepo;
        _publisherRepo = pubRepo;
    }
    public IAuthorRepository AuthorRepository => _authorRepository;

    public IBookAuthorRepository BookAuthorRepository => _bookAuthorRepo;

    public IBookRepository BookRepository => _bookRepo;

    public IUserRepository UserRepository => _userRepo;

    public IRoleRepository RoleRepository => _roleRepo;

    public IPublisherRepository PublisherRepository => _publisherRepo;

    public async Task<bool> SaveChangesAsync() => await _dbContext.SaveChangesAsync() >= 0;
    
        
    
}