namespace Dados_App.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly IHttpClientServico httpClientServico;
    public UsuarioRepositorio(IHttpClientServico httpClientServico)
    {
        this.httpClientServico = httpClientServico;
    }
    public Task Alterar(Usuario usuario)
    {
        throw new NotImplementedException();
    }

    public Task Excluir(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> Gravar(Usuario usuario)
    {
        return await httpClientServico.PostAsync<string>($"{Constates.EndPoint}/usuarios", usuario);
    }

    public async Task<TokenResposta> Verificar(Usuario usuario)
    {
        return await httpClientServico.PostAsync<TokenResposta>($"{Constates.EndPoint}/token", usuario);
    }
}
