namespace DESAFIO_MOVISIS.Models;

public class Configurar
{
    public Configuracao ObterConfiguracao { get; set; }

    public class Configuracao
    {
        public Apikeyimgbb ApiKeyIMGBB { get; set; }
    }

    public class Apikeyimgbb
    {
        public string Key { get; set; }
    }

}
