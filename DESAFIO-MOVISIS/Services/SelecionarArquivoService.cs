namespace DESAFIO_MOVISIS.Services;

public static class SelecionarArquivoService
{
    public static async Task<FileResult?> AbrirSelecionar()
    {
        try
        {
            var result = await FilePicker.Default.PickAsync();
            if (result != null)
            {
                if (result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase) ||
                    result.FileName.EndsWith("pdf", StringComparison.OrdinalIgnoreCase))
                {
                    using var stream = await result.OpenReadAsync();
                    var image = ImageSource.FromStream(() => stream);
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return null;
    }
}
