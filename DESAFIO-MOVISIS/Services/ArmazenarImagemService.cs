using DotNetEnv;

namespace DESAFIO_MOVISIS.Services;

public static class ArmazenarImagemService
{   
    
    private static readonly HttpClient _httpClient = new HttpClient();

    public async static Task<ArmazenarImagemResponse?> EnviarImagem(this string base64)
    {
        Env.Load();
        string? valor = Env.GetString("API_KEY"); 

        if (string.IsNullOrWhiteSpace(base64))
        {
            throw new ArgumentException("O parâmetro base64 não pode ser nulo ou vazio.", nameof(base64));
        }
        
        try
        {
            var content = new MultipartFormDataContent
                {
                    { new StringContent(base64), "image" }
                };

            var response = await _httpClient.PostAsync($"https://api.imgbb.com/1/upload?key=017e91965de2a809b5fc1c0f009f06d8", content);

            response.EnsureSuccessStatusCode();

            var conteudo = await response.Content.ReadAsStringAsync();
            var dados = JsonSerializer.Deserialize<ArmazenarImagemResponse>(conteudo);

            return dados;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro na requisição HTTP: {ex.Message}");
            return null;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Erro ao desserializar a resposta: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro inesperado: {ex.Message}");
            return null;
        }
    }

}
