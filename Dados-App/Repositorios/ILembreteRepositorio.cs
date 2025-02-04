namespace Dados_App.Repositorios;

public interface ILembreteRepositorio
{
    Task<List<LembreteResponse>> Todos(string token, Guid idUsuario);
    Task<string> Gravar(LembreteResponse lembrete, string token);
    Task<string> Alterar(LembreteResponse lembrete, string token);
    Task<string> Excluir(Guid guid, string token);
}
