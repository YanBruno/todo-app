using Flunt.Notifications;
using TodoApp.Core.Src.Commands.UserCommands;
using TodoApp.Core.Src.Entities;
using TodoApp.Core.Src.Repositories;
using TodoApp.Core.Src.Services;
using TodoApp.Core.Src.Utils;
using TodoApp.Core.Src.ValueObjects;
using TodoApp.Shared.Src.Commands;
using TodoApp.Shared.Src.Handlers;

namespace TodoApp.Core.Src.Handlers;

public class UserHandler
    : Notifiable<Notification>
    , IHandler<CreateUserCommand>
    , IHandler<UserLoginCommand>
    , IHandler<DeleteUserCommand>
{
    private readonly IPasswordService _passwordService;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserHandler(IPasswordService passwordService, IUserRepository userRepository, ITokenService tokenService)
    => (_passwordService, _userRepository, _tokenService) = (passwordService, userRepository, tokenService);

    public async Task<ICommandResult> HandleAsync(CreateUserCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command)) return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _userRepository.CheckEmail(command.EmailAddress))
            AddNotification("UserHandler", "Email existente");

        //Cria VO's
        var name = new Name(command.FirstName, command.LastName);
        var email = new Email(command.EmailAddress);
        var password = new Password(_passwordService.GenerateHash(command.Password));

        var user = new User(name, email, password);

        AddNotifications(name, email, password, user);

        if (!IsValid) return new GenericCommandResult(false, "Ops, algo errado aconteceu", Notifications);

        var success = await _userRepository.CreateAsync(user);
        var token = _tokenService.GenerateToken(user);

        if (!success) return new GenericCommandResult(false, "Ops, algo errado aconteceu durante a persistência de dados", null);

        return new GenericCommandResult(true, "Usuario criado com sucesso", new { token, user });
    }

    public async Task<ICommandResult> HandleAsync(UserLoginCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command))
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _userRepository.GetAsync(command.EmailAddress) is var user && user == null)
            return new GenericCommandResult(false, "Email ou senha inválido", null);

        if (!_passwordService.CheckPassword(user.GetPassword().Hash, command.Password))
            return new GenericCommandResult(false, "Email ou senha inválido", null);

        var token = _tokenService.GenerateToken(user);
        return new GenericCommandResult(true, "Login bem sucedido", new { token, user });
    }

    public async Task<ICommandResult> HandleAsync(DeleteUserCommand command)
    {
        if (!ValidateCommand.IsCommandValid(command))
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", command.Notifications);

        if (await _userRepository.GetAsync(command.UserId) is var user && user == null)
            return new GenericCommandResult(false, "Usuário ou senha inválido", null);

        if (!_passwordService.CheckPassword(user.GetPassword().Hash, command.Password))
            return new GenericCommandResult(false, "Usuário ou senha inválido", null);

        user.DeactiveAccount();
        
        AddNotifications(user);

        if(!IsValid)
            return new GenericCommandResult(false, "Ops, algo errado aconteceu", Notifications);

        return new GenericCommandResult(true, "Usuário removido", null);
    }
}
