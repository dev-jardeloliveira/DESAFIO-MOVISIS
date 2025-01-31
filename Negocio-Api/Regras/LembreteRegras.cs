namespace Negocio_Api.Regras;

public class LembreteRegras : AbstractValidator<Lembrete>
{
    public LembreteRegras(IUsuarioRepositorio repositorio)
    {

        _ = RuleFor(it => it.IdUsuario).NotNull().WithMessage("Campo IdUsuario nulo.")
            .NotEmpty().WithMessage("Campo IdUsuario vazio.");

        _ = RuleFor(it => it.Titulo)
            .NotNull().WithMessage("Campo titulo nulo.")
            .NotEmpty().WithMessage("Campo titulo vazio.");

        _ = RuleFor(it => it.Vencimento)
            .NotNull().WithMessage("Campo vencimento nulo.")
            .NotEmpty().WithMessage("Campo vencimento vazio.");

        _ = RuleFor(it => it.Hora)
            .NotNull().WithMessage("Campo hora nulo.")
            .NotEmpty().WithMessage("Campo hora vazio.");

    }

}
