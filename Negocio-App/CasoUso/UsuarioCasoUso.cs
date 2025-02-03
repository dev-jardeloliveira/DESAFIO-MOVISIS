namespace Negocio_App.CasoUso;

public class UsuarioCasoUso
{
    private readonly IUsuarioRepositorio repositorio;

    public UsuarioCasoUso(IUsuarioRepositorio repositorio)
    {
        this.repositorio = repositorio;
    }

    public async Task<string> Gravar(Usuario usuario)
    {
        return await repositorio.Gravar(usuario);
    }

    public async Task<TokenResposta> Verificar(Usuario usuario)
    {
        return await repositorio.Verificar(usuario);
    }
}
