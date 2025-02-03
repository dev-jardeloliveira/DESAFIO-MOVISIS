namespace Dados_App.Repositorios;

public interface ILembreteRepositorio
{
    Task<List<Lembrete>> Todos();
    Task<string> Gravar(Lembrete lembrete);
    Task<string> Alterar(Lembrete lembrete);
    Task<string> Excluir(int id);
}
