namespace Api_Lembrete.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{ 
    private readonly UsuarioCasoUso casoUso;

    public UsuariosController(UsuarioCasoUso casoUso)
    {
        this.casoUso = casoUso;
    }

    [HttpGet("{email}")]
    public async Task<Usuario?> Get(string email)
    {
        return await casoUso.Filtrar(email);
    }

    [HttpPost]
    public async Task<string> Post([FromBody] Usuario usuario)
    {
        return await casoUso.Gravar(usuario);
    }

    [HttpPut]
    public async Task<string> Put([FromBody] Usuario usuario)
    {
        return await casoUso.Alterar(usuario);
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(Guid id)
    {
        return await casoUso.Excluir(id);
    }
}
