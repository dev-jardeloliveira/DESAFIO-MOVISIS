namespace DESAFIO_MOVISIS.ViewModels.ModelsDto;

public class LembreteDto
{
    public Guid Id { get; set; }
    public string? IdUsuario { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get; set; }
    public DateTime? Vencimento { get; set; }
    public TimeSpan? Hora { get; set; }
    public string? Anexo { get; set; }
}
