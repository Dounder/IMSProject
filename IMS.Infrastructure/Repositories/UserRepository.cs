using AutoMapper;
using IMS.Domain.Entities;
using IMS.Domain.Interfaces;
using IMS.Infrastructure.Data;

namespace IMS.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context, IMapper mapper) : BaseRepository<User>(context, mapper), IUserRepository
{
}