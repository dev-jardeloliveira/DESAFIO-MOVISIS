
namespace Data_Api.Repositorio;

public class LembreteRepository : ILembreteRepositorio
{
    private readonly DataContexto contexto;

    public LembreteRepository(DataContexto contexto)
    {
        this.contexto = contexto;
    }

    public Task<int> Alterar(Lembrete lembrete)
    {
        contexto.Entry(lembrete).State = EntityState.Modified;
        contexto.Lembrete.Update(lembrete);
        return contexto.SaveChangesAsync();
    }

    public async Task<int> Excluir(Guid id)
    {
        Lembrete? lembrete = await contexto.Lembrete.FindAsync(id);
        if (lembrete is null)
            return 0;

        contexto.Lembrete.Remove(lembrete);
        return await contexto.SaveChangesAsync();
    }

    public async Task<List<Lembrete>> Filtrar(Guid idUsuario)
    {
        return await contexto.Lembrete
                .Where(it => it.IdUsuario == idUsuario)
                .OrderBy(it => it.Vencimento)
                .ThenBy(it => it.Hora)
                .ToListAsync();
    }

    public Task<int> Gravar(Lembrete lembrete)
    {
        contexto.Lembrete.Add(lembrete);
        return contexto.SaveChangesAsync();
    }
}
