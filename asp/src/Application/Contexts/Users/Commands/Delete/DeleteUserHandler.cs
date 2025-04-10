using Application.Contexts.Users.Repositories;
using MediatR;

namespace Application.Contexts.Users.Commands.Delete;

public class DeleteUserHandler: IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand request,
    CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        await _userRepository.DeleteAsync(entity!, cancellationToken);
    }
}