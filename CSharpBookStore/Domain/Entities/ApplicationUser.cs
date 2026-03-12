using Microsoft.AspNetCore.Identity;

namespace CSharpBookStore.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? FullName { get; private set; }
    public DateTime RegisteredAt { get; private set; } = DateTime.UtcNow;

    public ApplicationUser() { }

    public static ApplicationUser Create(string email, string? fullName = null)
    {
        if (string.IsNullOrEmpty(email)) throw new ArgumentException("Email is required", nameof(email));

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName?.Trim(),
            RegisteredAt = DateTime.UtcNow
        };

        return user;
    }

    public void UpdateFullName(string? newFullName)
    {
        FullName = newFullName?.Trim();
    }

}