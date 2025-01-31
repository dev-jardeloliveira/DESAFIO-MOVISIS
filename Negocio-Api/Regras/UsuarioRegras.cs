namespace Negocio_Api.Regras;

public class UsuarioRegras : AbstractValidator<Usuario>
{
    private readonly IUsuarioRepositorio repositorio;
    public UsuarioRegras(IUsuarioRepositorio repositorio)
    {
        this.repositorio = repositorio;

        _ = RuleFor(it => it.Email).NotNull().WithMessage("Campo email nulo.")
            .NotEmpty().WithMessage("Campo email vazio.")
            .EmailAddress().WithMessage("Campo email inválido.");

        _ = RuleFor(it => it.Senha)
            .NotNull().WithMessage("Campo senha nulo.")
            .NotEmpty().WithMessage("Campo senha vazio.")
            .MinimumLength(6).WithMessage("Campo senha menor que 6 digitos.");
    }

    public async Task<bool> EmailUnico(string email)
    {
       return !await repositorio.Validar(email);
    }
}
