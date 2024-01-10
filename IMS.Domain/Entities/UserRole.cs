using IMS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IMS.Domain.Entities;

public class UserRole : IdentityRole<int>, IBaseEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}