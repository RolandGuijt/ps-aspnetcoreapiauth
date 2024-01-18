using System.Net.Http.Headers;
using System.Net.Http.Json;

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7274");

await httpClient.PostAsJsonAsync("/register", 
    new User { Username = "roland", Password = "Secret123!", Email = "roland.guijt@gmail.com" });

var loginResult = await httpClient.PostAsJsonAsync("/login", 
    new Login { Email = "roland.guijt@gmail.com", Password = "Secret123!" });

var response = await loginResult.Content.ReadFromJsonAsync<LoginResponse>();

httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", response.AccessToken);
var userResponse = await httpClient.GetAsync("/user");

Console.WriteLine(await userResponse.Content.ReadAsStringAsync());
Console.ReadKey();

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

public class Login
{
    public string Password { get; set; }
    public string Email { get; set; }
}