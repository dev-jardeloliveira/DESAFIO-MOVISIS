namespace Data_Api.Model;

public class Lembrete
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid IdUsuario { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Titulo { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Descricao { get; set; }

    [Required]
    public DateTime? Vencimento { get; set; }

    [Required]
    public TimeSpan? Hora { get; set; }
    public string? Anexo { get; set; }

}
