using CSharpBookStore.Model;

namespace CSharpBookStore.Infrastructure.Persistence;

public class BookEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Pages { get; set; }
    public string Description { get; set; } = string.Empty;

    public static BookEntity FromDomain(Book model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Author = model.Author,
        Pages = model.Pages,
        Description = model.Description
    };

    public Book ToBook() => Book.Create(Id, Name, Author, Pages, Description);
}