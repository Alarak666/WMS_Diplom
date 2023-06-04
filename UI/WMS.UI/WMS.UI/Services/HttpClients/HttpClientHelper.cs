namespace WMS.UI.Services.HttpClients;

public class HttpClientHelper
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public HttpClientHelper()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true);

        _configuration = builder.Build();
        //   var jwt = new JwtHelper(_configuration);
        var jwtToken = _configuration["JWT:value"];

        _httpClient = new HttpClient();
        var url = _configuration.GetSection("API").Value;
        _httpClient.BaseAddress = new Uri(url);

        //_httpClient.DefaultRequestHeaders.Authorization =
        //    new AuthenticationHeaderValue("Bearer", jwtToken);
    }

    public async Task<HttpResponseMessage> Post(string url, HttpContent content, CancellationToken cancellation)
    {
        return await _httpClient.PostAsync(url, content, cancellation);
    }

    public async Task<HttpResponseMessage> Get(string url,CancellationToken cancellation)
    {
        return await _httpClient.GetAsync(url, cancellation);
    }


    public async Task<HttpResponseMessage> Put(string url, HttpContent content, CancellationToken cancellationToken)
    {
        return await _httpClient.PutAsync(url, content, cancellationToken);
    }

    public async Task<HttpResponseMessage> Delete(string url, CancellationToken cancellation)
    {
        return await _httpClient.DeleteAsync(url, cancellation);
    }
}