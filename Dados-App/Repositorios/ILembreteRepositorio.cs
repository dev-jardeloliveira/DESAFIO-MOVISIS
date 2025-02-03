namespace Dados_App.Repositorios;

public interface ILembreteRepositorio
{
    Task<List<Lembrete>> Todos(string token);
    Task<string> Gravar(Lembrete lembrete, string token);
    Task<string> Alterar(Lembrete lembrete, string token);
    Task<string> Excluir(Guid guid, string token);
}
