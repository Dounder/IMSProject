using IMS.Domain.Interfaces;
using MediatR;

namespace IMS.Application.UseCases.Auth.Commands;

public class RenewTokenCommandHandler(ITokenService tokenService) : IRequestHandler<RenewTokenCommand, string>
{
    public async Task<string> Handle(RenewTokenCommand request, CancellationToken cancellationToken)
    {
        return await tokenService.RenewAccessToken(request.Username, request.RefreshToken);
    }
}