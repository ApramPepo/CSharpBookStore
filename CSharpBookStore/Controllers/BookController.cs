using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using CSharpBookStore.Domain.Entities;
using CSharpBookStore.Model;
using CSharpBookStore.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;

namespace CSharpBookStore.Controllers;

[Route("api/books")]
[ApiController]
[Authorize]
public class BookController : ControllerBase
{
    private readonly BookstoreDbContext _Db;

    public BookController(BookstoreDbContext Db) => _Db = Db;

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var entities = await _Db.Books.ToListAsync(ct);
        var books = entities.Select(e => e.ToBook()).ToList();
        return Ok(books);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByID(Guid id, CancellationToken ct)
    {
        var entity = await _Db.Books.FindAsync([id], ct);
        if(entity is null) return NotFound();
        return Ok(entity.ToBook());
    }

    public class CreateBookRequest
    {
        public string Name { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int Pages { get; set; }
        public string? Description { get; set; } = null!;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookRequest request, CancellationToken ct)
    {
        var book = Book.Create(Guid.NewGuid(), request.Name, request.Author, request.Pages, request.Description);

        var entity = BookEntity.FromDomain(book);
        _Db.Books.Add(entity);
        await _Db.SaveChangesAsync(ct);

        return CreatedAtAction(nameof(GetByID), new { id = book.Id, }, book);
    }

    [HttpPut("{id:guid}/description")]
    public async Task<IActionResult> UpdateDescription(Guid id, [FromBody] string? newDescription, CancellationToken ct)
    {
        var entity = await _Db.Books.FindAsync([id], ct);
        if(entity is null) return NotFound();

        var book = entity.ToBook();
        book.UpdateDescription(newDescription);

        entity.Description = book.Description;
        await _Db.SaveChangesAsync(ct);

        return Ok(book);
    }
}
