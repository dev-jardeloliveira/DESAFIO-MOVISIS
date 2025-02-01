namespace Dados_App.Repositorios;

public interface IUsuarioRepositorio
{
    Task<TokenResposta> Verificar(Usuario usuario);
    Task<string> Gravar(Usuario usuario);
    Task Alterar(Usuario usuario);
    Task Excluir(int id);
}
