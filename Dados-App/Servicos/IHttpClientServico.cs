namespace Dados_App.Servicos;

public interface IHttpClientServico
{
    void SetToken(string token);
    Task<T> GetAsync<T>(string uri);
    Task<T> PostAsync<T>(string uri, object data);
    Task<T> PutAsync<T>(string uri, object data);
    Task<T> DeleteAsync<T>(string uri);
}
