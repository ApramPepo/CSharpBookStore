namespace CSharpBookStore.Model;

public class Book
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Author { get; private set; }
    public int Pages { get; private set; }
    public string Description { get; private set; } = string.Empty;

    private Book() { }

    private Book(Guid id, string name, string description, string author, int pages)
    {
        Id = id;
        Name = name;
        Description = description;
        Author = author;
        Pages = pages;
    }

    public static Book Create(Guid Id, string name, string author, int pages, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
        var finalName = name.Trim();
        if (finalName.Length > MaxNameLength) throw new ArgumentException($"Name max {MaxNameLength} characters.", nameof(name));

        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author is required.", nameof(author));
        var finalAuthor = author.Trim();
        if (finalAuthor.Length > MaxAuthorLength) throw new ArgumentException($"Author max {MaxAuthorLength} characters.", nameof(author));

        if (pages <= 0) throw new ArgumentException("Pages must be positive number.", nameof(pages));
        if (pages > MaxPages) throw new ArgumentException("Pages are too large.", nameof(pages));

        var finalDescription = (description ?? string.Empty).Trim();
        if (finalDescription.Length > MaxDescriptionLength) throw new ArgumentException($"Description max {MaxDescriptionLength} characters.", nameof(description));
        return new(Guid.NewGuid(), name: finalName, description: finalDescription, author: finalAuthor, pages: pages);
    }

    public const int MaxNameLength = 100;
    public const int MaxAuthorLength = 100;
    public const int MaxPages = 10000;
    public const int MaxDescriptionLength = 250;

    public void UpdateDescription(string? newDescription)
    {
        var trimmedDescription = (newDescription ?? String.Empty).Trim();
        if (trimmedDescription.Length > MaxDescriptionLength) throw new ArgumentException($"Description max {MaxDescriptionLength} characters.", nameof(newDescription));
        Description = trimmedDescription;
    }

    public void ChangeAuthor(string newAuthor)
    {
        if (String.IsNullOrEmpty(newAuthor)) throw new ArgumentException("Author is required.", nameof(newAuthor));
        var trimmedAuthor = newAuthor.Trim();
        if (trimmedAuthor.Length > MaxAuthorLength) throw new ArgumentException($"Author max {MaxAuthorLength} characters.", nameof(newAuthor));
        Author = trimmedAuthor;
    }

}