using eBookStore.Services.Repositories;

namespace eBookStore.Services;
public interface IUnitOfWork
{
    public IAuthorRepository AuthorRepository { get; }
    public IBookAuthorRepository BookAuthorRepository { get; }
    public IBookRepository BookRepository { get; }
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    public IPublisherRepository PublisherRepository { get; }
    Task<bool> SaveChangesAsync();
}