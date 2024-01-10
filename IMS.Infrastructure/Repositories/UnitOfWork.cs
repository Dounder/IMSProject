using AutoMapper;
using IMS.Domain.Interfaces;
using IMS.Infrastructure.Data;

namespace IMS.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context, IMapper mapper) : IUnitOfWork
{
    public IUserRepository UserRepository => new UserRepository(context, mapper);
    
    public async Task CommitAsync() => await context.SaveChangesAsync();

    public void Dispose() => context.Dispose();
}