namespace Dados_App.Repositorios;

public class LembreteRepositorio : ILembreteRepositorio
{
    private readonly IHttpClientServico httpClientServico;

    public LembreteRepositorio(IHttpClientServico httpClientServico)
    {
        this.httpClientServico = httpClientServico;
    }

    public Task<string> Alterar(LembreteResponse lembrete, string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.PutAsync<string>($"{Constates.EndPoint}/lembretes", lembrete);
    }

    public Task<string> Excluir(Guid guid, string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.DeleteAsync<string>($"{Constates.EndPoint}/lembretes/{guid}");
    }

    public Task<string> Gravar(LembreteResponse lembrete, string token)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.PostAsync<string>($"{Constates.EndPoint}/lembretes", lembrete);
    }

    public Task<List<LembreteResponse>> Todos(string token, Guid idUsuario)
    {
        httpClientServico.SetToken(token);
        return httpClientServico.GetAsync<List<LembreteResponse>>($"{Constates.EndPoint}/lembretes/{idUsuario}");
    }
}
