namespace Negocio_Api.CasoDeUso;

public class UsuarioCasoUso
{
    private readonly IUsuarioRepositorio repositorio;
    private readonly UsuarioRegras regras;

    public UsuarioCasoUso(IUsuarioRepositorio repository, UsuarioRegras regras)
    {
        this.repositorio = repository;
        this.regras = regras;
    }

    public async Task<string> Alterar(Usuario usuario)
    {
        if (!regras.Validate(usuario).IsValid)
        {
            return "Dados invalido!";
        }


        int resultado = await repositorio.Alterar(usuario);

        return resultado == 1 ? "Sucesso" : "Erro";
    }

    public async Task<string> Gravar(Usuario usuario) 
    {
        if (!regras.Validate(usuario).IsValid)
        {
            return "Dados invalido!";
        }

        bool emailUnico = await regras.EmailUnico(usuario.Email!);
        if (!emailUnico)
        {
            return "Já cadastrado";
        }

        int resultado =  await repositorio.Gravar(usuario);

        return resultado == 1 ? "Sucesso" : "Erro";
    }

    public async Task<Usuario?> Filtrar(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return null;
        }

        return await repositorio.Filtrar(email);
    }

    public async Task<int> Excluir(Guid id)
    {
        return await repositorio.Excluir(id);
    }
}
