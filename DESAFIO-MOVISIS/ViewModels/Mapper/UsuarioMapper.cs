namespace DESAFIO_MOVISIS.ViewModels.Mapper;

public static class UsuarioMapper
{
    public static UsuarioDto ToDto(Autenticar autenticar)
    {
        return new UsuarioDto
        {
            Id = autenticar.Id,
            Email = autenticar.Email,
            Senha = autenticar.Senha
        };
    }

    public static UsuarioDto ToDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            Id = Guid.TryParse(usuario.Guid, out var guid) ? guid : Guid.Empty,
            Email = usuario.Email,
            Senha = usuario.Senha,
            Token = usuario.Token
        };
    }

    public static Autenticar ToAutenticar(UsuarioDto dto)
    {
        return new Autenticar(dto.Id, dto.Email ?? "", dto.Senha ?? "");
    }

    public static Usuario ToUsuario(UsuarioDto dto)
    {
        return new Usuario
        {
            Guid = dto.Id.ToString(),
            Email = dto.Email,
            Senha = dto.Senha,
            Token = dto.Token
        };
    }
}

