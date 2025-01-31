namespace Dados_App.Servicos;

public class HttpClientServico : IHttpClientServico, IDisposable
{
    private readonly HttpClient _httpClient;
    private bool _disposed = false;

    public HttpClientServico()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://api.exemplo.com/")
        };
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public void SetToken(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode(); 
        var content = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(content)!;
    }

    public async Task<T> PostAsync<T>(string uri, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(uri, content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent)!;
    }

    public async Task<T> PutAsync<T>(string uri, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(uri, content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent)!;
    }

    public async Task<T> DeleteAsync<T>(string uri)
    {
        var response = await _httpClient.DeleteAsync(uri);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(responseContent)!;
    }
     
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
            _disposed = true;
        }
    }

    ~HttpClientServico()
    {
        Dispose(false);
    }
}
