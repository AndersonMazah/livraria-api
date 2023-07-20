using Livraria.Core.Application.Ports;

namespace Livraria.Infra.Auth;

public class UserService : IUserService
{
    private readonly IClientesApplication _clientesApplication;

    public UserService(IClientesApplication clientesApplication)
    {
        _clientesApplication = clientesApplication;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        if (username.Equals("admin") && password.Equals("admin"))
        {
            return new User("admin");
        }

        if (await _clientesApplication.VerificarLoginDeClienteAsync(username, password))
        {
            return new User(username);
        }

        return null;
    }

}