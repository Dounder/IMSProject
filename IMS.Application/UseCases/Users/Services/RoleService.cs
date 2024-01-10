using AutoMapper;
using IMS.Application.UseCases.Users.DTOs;
using IMS.Domain.Entities;
using IMS.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace IMS.Application.UseCases.Users.Services;

public class RoleService(UserManager<User> userManager, RoleManager<UserRole> roleManager, IMapper mapper, IMemoryCache cache)
{
    public async Task AddRoleToUser(User user, List<string> roles)
    {
        foreach (var roleName in roles.Distinct())
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null) throw new BadRequestException($"Role {roleName} does not exist");

            var result = await userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded) throw new BadRequestException($"Failed to add role {roleName} to user");
        }
    }

    public async Task<IEnumerable<TM>> GetAllRoles<TM>(int userId)
    {
        var cacheKey = $"UserRoles_{userId}";
        if (cache.TryGetValue(cacheKey, out IEnumerable<TM> roles)) return roles ?? Enumerable.Empty<TM>();

        // Data not in cache, so load data.
        var rolesNames = await userManager.GetRolesAsync(new User { Id = userId });
        var rolesData = roleManager.Roles.Where(x => rolesNames.Contains(x.Name!));
        roles = mapper.Map<IEnumerable<TM>>(rolesData);

        // Set cache options.
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromDays(1)); // or use SetAbsoluteExpiration

        // Save data in cache.
        var allRoles = roles.ToList();
        cache.Set(cacheKey, allRoles, cacheEntryOptions);

        return allRoles;
    }

    public async ValueTask UpdateRoles(User user, List<RoleDto> roles, CancellationToken cancellationToken = default)
    {
        if (roles.Count == 0) return;

        var newRoles = roles.Select(r => r.Name).ToList();
        var currentRoles = await userManager.GetRolesAsync(user);

        var removeRolesResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeRolesResult.Succeeded) throw new BadRequestException("Error while updating user roles");

        var addRolesResult = await userManager.AddToRolesAsync(user, newRoles);
        if (!addRolesResult.Succeeded) throw new BadRequestException("Error while updating user roles");
    }
}