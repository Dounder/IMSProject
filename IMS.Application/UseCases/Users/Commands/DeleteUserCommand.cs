using MediatR;

namespace IMS.Application.UseCases.Users.Commands;

public class DeleteUserCommand(int id) : IRequest<Unit>
{
    public int Id { get; set; } = id;
}