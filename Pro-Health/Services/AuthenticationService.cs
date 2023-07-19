using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProHealth.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;

    public bool Authenticated { get; private set; }

    public AuthenticationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> Authenticate(string username, string password)
    {
        string response = await _httpClient.GetStringAsync("/data/credentials.json");

        IEnumerable<Credentials> credentials = JsonSerializer.Deserialize<IEnumerable<Credentials>>(response) ?? Enumerable.Empty<Credentials>();

        if (!credentials.Any(c => c.Username.ToLower() == username.ToLower()))
        {
            return Authenticated = false;
        }

        string actualPassword = credentials.First(c => c.Username.ToLower() == username.ToLower()).Password;

        if (password != DecryptString(actualPassword))
        {
            return Authenticated = false;
        }

        return Authenticated = true;
    }

    private static string EncryptString(string plainText)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
    }

    private static string DecryptString(string cipherText)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(cipherText));
    }

    class Credentials
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }
}
