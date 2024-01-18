using System.Net.Http.Headers;
using System.Net.Http.Json;

var httpClient = new HttpClient();
httpClient.BaseAddress = new Uri("https://localhost:7274");

var loginResult = await httpClient.GetAsync("/login?username=Roland");

var response = await loginResult.Content
    .ReadFromJsonAsync<LoginResponse>();

httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", response.AccessToken);
var userResponse = await httpClient.GetAsync("/user");

Console.WriteLine(await userResponse.Content.ReadAsStringAsync());
Console.ReadKey();

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
}