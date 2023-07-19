namespace ProHealth.Services.Abstractions;

public interface IAuthenticationService
{
    bool Authenticated { get; }

    Task<bool> Authenticate(string username, string password);
}
