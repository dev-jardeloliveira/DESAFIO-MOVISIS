namespace Data_Api.Repositorio;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly DataContexto contexto;

    public UsuarioRepositorio(DataContexto contexto)
    {
        this.contexto = contexto;
    }

    public Task<int> Alterar(Usuario usuario)
    {
        contexto.Entry(usuario).State = EntityState.Modified;
        contexto.Usuario.Update(usuario);
        return contexto.SaveChangesAsync();
    }

    public async Task<int> Excluir(Guid id)
    {
        Usuario? usuario = await contexto.Usuario.FindAsync(id);  
        if(usuario is null)
            return 0;

        contexto.Usuario.Remove(usuario);
        return await contexto.SaveChangesAsync();
    }

    public async Task<Usuario?> Filtrar(string email)
    {
        return await contexto.Usuario.FirstOrDefaultAsync(it=>it.Email.Equals(email));
    }

    public Task<int> Gravar(Usuario usuario)
    {
        contexto.Usuario.Add(usuario);
        return contexto.SaveChangesAsync();
    }

    public async Task<bool> Validar(string email)
    {
        if (email is null)
            return false;

        return await contexto.Usuario.AnyAsync(it=>it.Email.Equals(email));
    }
}
