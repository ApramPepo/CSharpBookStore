using CSharpBookStore.Model;

namespace CSharpBookStore.Infrastructure.Persistence;

public interface IBookRepository
{
    Task<Book> GetByIDAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(Book book, CancellationToken ct = default);
    void Update(Book book);
    Task DeleteAsync(Book book, CancellationToken ct = default);
}