namespace DESAFIO_MOVISIS.Models;

public class Lembrete
{
    public Guid Id { get; set; }
    public string? Titulo { get; set; }
    public string? Descricao { get ; set; }
    public DateTime? Vencimento { get; set; }
    public TimeSpan? Hora { get; set; }
    public string? Anexo { get; set; }
   
}
