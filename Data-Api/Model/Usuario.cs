namespace Data_Api.Model;

[Index(nameof(Email), IsUnique = true)]
public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    [MaxLength(20)]
    public string? Senha { get; set; }
}
