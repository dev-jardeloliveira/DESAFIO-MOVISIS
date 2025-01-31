namespace Data_Api.Repositorio;

public interface ILembreteRepositorio
{
    Task<List<Lembrete>> Filtrar(Guid idUsuario);
    Task<int> Gravar(Lembrete lembrete);
    Task<int> Alterar(Lembrete lembrete);
    Task<int> Excluir(Guid id);
}
