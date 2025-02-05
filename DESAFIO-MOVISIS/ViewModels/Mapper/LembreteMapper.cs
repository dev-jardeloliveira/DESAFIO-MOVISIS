namespace DESAFIO_MOVISIS.ViewModels.Mapper;

public static class LembreteMapper
{
    public static Lembrete MapearParaModelo(LembreteDto lembreteDTO)
    {
        return new Lembrete
        {
            Id = lembreteDTO.Id,
            IdUsuario = lembreteDTO.IdUsuario,
            Titulo = lembreteDTO.Titulo,
            Descricao = lembreteDTO.Descricao,
            Vencimento = lembreteDTO.Vencimento,
            Hora = lembreteDTO.Hora,
            Anexo = lembreteDTO.Anexo
        };
    }

    public static LembreteDto MapearParaDTO(Lembrete lembrete)
    {
        return new LembreteDto
        {
            Id = lembrete.Id,
            IdUsuario = lembrete.IdUsuario,
            Titulo = lembrete.Titulo,
            Descricao = lembrete.Descricao,
            Vencimento = lembrete.Vencimento,
            Hora = lembrete.Hora,
            Anexo = lembrete.Anexo
        };
    }

    public static Dados_App.Modelo.LembreteResponse MapearParaLembreteDados(Lembrete lembrete)
    {
        return new Dados_App.Modelo.LembreteResponse
        {
            id = lembrete.Id.ToString(),
            idUsuario = lembrete.IdUsuario,
            titulo = lembrete.Titulo,
            descricao = lembrete.Descricao,
            vencimento = (DateTime)lembrete.Vencimento!,
            hora = lembrete.Hora.ToString(),
            anexo = lembrete.Anexo
        };
    }

    public static Lembrete MapearResponseParaModelo(Dados_App.Modelo.LembreteResponse lembreteResponse)
    {
        return new Lembrete
        {
            Id = Guid.TryParse(lembreteResponse.id, out Guid resultado)?resultado:Guid.NewGuid(),
            IdUsuario = lembreteResponse.idUsuario,
            Titulo = lembreteResponse.titulo,
            Descricao = lembreteResponse.descricao,
            Vencimento = lembreteResponse.vencimento,
            Hora = TimeSpan.TryParse(lembreteResponse.hora,out TimeSpan resultado2)?resultado2:null,
            Anexo = lembreteResponse.anexo
        };
    }

    public static LembreteDto MapearResponseParaDto(Dados_App.Modelo.LembreteResponse lembreteResponse)
    {
        return new LembreteDto
        {
            Id = Guid.TryParse(lembreteResponse.id, out Guid resultado) ? resultado : Guid.NewGuid(),
            IdUsuario = lembreteResponse.idUsuario,
            Titulo = lembreteResponse.titulo,
            Descricao = lembreteResponse.descricao,
            Vencimento = lembreteResponse.vencimento,
            Hora = TimeSpan.TryParse(lembreteResponse.hora, out TimeSpan resultado2) ? resultado2 : null,
            Anexo = lembreteResponse.anexo
        };
    }

}
