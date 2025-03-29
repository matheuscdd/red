using Application.Contexts.Users.Dtos;
using MediatR;

namespace Application.Contexts.Users.Commands.Create;

public class CreateUserCommand : IRequest<UserDto>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}