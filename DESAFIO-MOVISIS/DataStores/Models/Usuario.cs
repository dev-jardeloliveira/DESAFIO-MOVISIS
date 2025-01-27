namespace DESAFIO_MOVISIS.DataStores.Models;

public class Usuario
{
    [PrimaryKey]
    public string? Guid { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? Token { get; set; }
}
