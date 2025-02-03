namespace Negocio_App.CasoUso;

public class LembreteCasoUso
{
    private readonly ILembreteRepositorio repositorio;

    public LembreteCasoUso(ILembreteRepositorio repositorio)
    {
        this.repositorio = repositorio;
    }

    public Task<string> Alterar(Lembrete lembrete, string token)
    {
        return repositorio.Alterar(lembrete, token);
    }

    public Task<string> Excluir(Guid guid, string token)
    {
        return repositorio.Excluir(guid, token);
    }

    public Task<string> Gravar(Lembrete lembrete, string token)
    {
        return repositorio.Gravar(lembrete, token);
    }

    public Task<List<Lembrete>> Todos(string token)
    {
        return repositorio.Todos(token);
    }
}
