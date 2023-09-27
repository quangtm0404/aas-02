using eBookStore.Domains.Entities;
using eBookStore.Repositories.Data;
using eBookStore.Services.Repositories;
using eBookStore.Services.Services.Interfaces;

namespace eBookStore.Repositories.Repositories;
public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
{
    public BookAuthorRepository(AppDbContext dbContext, ICurrentTime currentTime, IClaimsService claimsService)
    : base(dbContext, currentTime, claimsService){ }
}