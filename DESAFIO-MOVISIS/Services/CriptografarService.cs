namespace DESAFIO_MOVISIS.Services;

public static class CriptografarService
{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("2f0de361846cfaff01e1257c8bf1a65faffca6e17623ab510f12664dd463c941");
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("8b7c6ace50e57ab70dfab6f647dabf88d181bf5421b40050432b6a1b58c59e78"); 

    public static string Criptografar(string texto)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream stream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(stream))
                    {
                        swEncrypt.Write(texto);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
}
