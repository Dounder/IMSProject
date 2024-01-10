using IMS.Application.UseCases.Users.DTOs;
using MediatR;

namespace IMS.Application.UseCases.Users.Queries;

public class GetUserByIdQuery(int id) : IRequest<UserDto>
{
    public int Id { get; set; } = id;
}