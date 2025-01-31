namespace DESAFIO_MOVISIS.Validators;

public class LembreteValida : AbstractValidator<Lembrete>
{
    public LembreteValida()
    {
        RuleFor(x => x.Titulo).NotEmpty().NotNull();
        RuleFor(x => x.Descricao).NotEmpty().NotNull();
        RuleFor(x => x.Vencimento).NotEmpty().NotNull();
        RuleFor(x => x.Hora).NotEmpty().NotNull();
    }
}
