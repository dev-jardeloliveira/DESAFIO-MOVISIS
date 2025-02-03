namespace Dados_App.Repositorios;

public class LembreteRepositorio : ILembreteRepositorio
{
    private readonly IHttpClientServico httpClientServico;

    public LembreteRepositorio(IHttpClientServico httpClientServico)
    {
        this.httpClientServico = httpClientServico;
    }

    public Task<string> Alterar(Lembrete lembrete)
    {
        return httpClientServico.PutAsync<string>("lembrete", lembrete);
    }

    public Task<string> Excluir(int id)
    {
        return httpClientServico.DeleteAsync<string>($"lembrete/{id}");
    }

    public Task<string> Gravar(Lembrete lembrete)
    {
        return httpClientServico.PostAsync<string>("lembrete", lembrete);
    }

    public Task<List<Lembrete>> Todos()
    {
        return httpClientServico.GetAsync<List<Lembrete>>("");
    }
}
