namespace Negocio_Api.CasoDeUso;

public class LembreteCasoUso
{
    private readonly ILembreteRepositorio repositorio;
    private readonly LembreteRegras regras;

    public LembreteCasoUso(ILembreteRepositorio repositorio, LembreteRegras regras)
    {
        this.repositorio = repositorio;
        this.regras = regras;
    }

    public async Task<string> Alterar(Lembrete lembrete)
    {
        if (!regras.Validate(lembrete).IsValid)
        {
            return "Dados invalido!";
        }


        int resultado = await repositorio.Alterar(lembrete);

        return resultado == 1 ? "Sucesso" : "Erro";
    }

    public async Task<string> Gravar(Lembrete lembrete)
    {
        if (!regras.Validate(lembrete).IsValid)
        {
            return "Dados invalido!";
        }

        int resultado = await repositorio.Gravar(lembrete);

        return resultado == 1 ? "Sucesso" : "Erro";
    }

    public async Task<List<Lembrete>> Filtrar(Guid idUsuario)
    {
        return await repositorio.Filtrar(idUsuario);
    }

    public async Task<int> Excluir(Guid id)
    {
        return await repositorio.Excluir(id);
    }
}
