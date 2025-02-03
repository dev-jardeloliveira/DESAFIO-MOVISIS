namespace Dados_App.Repositorios;

public class LembreteRepositorio : ILembreteRepositorio
{
    private readonly IHttpClientServico httpClientServico;

    public LembreteRepositorio(IHttpClientServico httpClientServico)
    {
        this.httpClientServico = httpClientServico;
    }

    public Task<string> Alterar(Lembrete lembrete, string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.PutAsync<string>("/lembrete", lembrete);
    }

    public Task<string> Excluir(Guid guid, string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.DeleteAsync<string>($"/lembrete/{guid}");
    }

    public Task<string> Gravar(Lembrete lembrete, string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.PostAsync<string>("/lembrete", lembrete);
    }

    public Task<List<Lembrete>> Todos(string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.GetAsync<List<Lembrete>>("/lembrete");
    }
}
