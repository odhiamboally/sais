namespace DP.Api.Configuration;

public class ApiSettings
{
    public string? BaseUrl { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool UseHttps { get; set; } = true;
}
