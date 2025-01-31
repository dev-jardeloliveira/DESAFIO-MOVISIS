namespace Api_Lembrete.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LembretesController : ControllerBase
{
    private readonly LembreteCasoUso casoUso;

    public LembretesController(LembreteCasoUso casoUso)
    {
        this.casoUso = casoUso;
    }

    [HttpGet("{idUsuario}")]
    public async Task<List<Lembrete>?> Get(Guid idUsuario)
    {
        return await casoUso.Filtrar(idUsuario);
    }

    [HttpPost]
    public async Task<string> Post([FromBody] Lembrete lembrete)
    {
        return await casoUso.Gravar(lembrete);
    }

    [HttpPut]
    public async Task<string> Put([FromBody] Lembrete lembrete)
    {
        return await casoUso.Alterar(lembrete);
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(Guid id)
    {
        return await casoUso.Excluir(id);
    }
}
