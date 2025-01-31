namespace Data_Api.Repositorio;

public interface IUsuarioRepositorio
{
    Task<Usuario> Filtrar(string email);
    Task<bool> Validar(string email);
    Task<int> Gravar(Usuario usuario);
    Task<int> Alterar(Usuario usuario);
    Task<int> Excluir(Guid id);
}
