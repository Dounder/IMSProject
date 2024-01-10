using System.ComponentModel.DataAnnotations;
using IMS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IMS.Domain.Entities;

public class User : IdentityUser<int>, IBaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<UserRole> Roles { get; set; } = new();

    // RefreshToken
    [MaxLength(1000)] public string RefreshToken { get; set; } = default!;
    public DateTime RefreshTokenExpiryTime { get; set; } = default!;
}