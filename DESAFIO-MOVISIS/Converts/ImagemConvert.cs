namespace DESAFIO_MOVISIS.Converts;

public static class ImagemConverte
{
    public static async Task<string> ConverterBase64(this FileResult? imagem)
    {
        Stream stream = await imagem?.OpenReadAsync()!;
        var bytes = ReadFully(stream);
        var base64 = Convert.ToBase64String(bytes);
        return base64;
    }

    private static byte[] ReadFully(Stream input)
    {
        MemoryStream ms = new MemoryStream();
        input.CopyTo(ms);
        return ms.ToArray();
    }
}
