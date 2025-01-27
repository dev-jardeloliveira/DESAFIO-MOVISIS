namespace DESAFIO_MOVISIS.Extensions;

public static class StringsExtension
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);


    public static bool IsValidarEmail(this string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        return EmailRegex.IsMatch(email);
    }

    public static bool IsValidarString(this string texto, int comprimentoMin)
    {
        if (string.IsNullOrWhiteSpace(texto))
            return false;

        return texto.Length >= comprimentoMin;
    }

    public static bool IsValidarConteudoDosCampos(this string senha, string confirmaSenha)
    {
        if (string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(confirmaSenha))
            return false;

        return senha.Equals(confirmaSenha);
    }
}
