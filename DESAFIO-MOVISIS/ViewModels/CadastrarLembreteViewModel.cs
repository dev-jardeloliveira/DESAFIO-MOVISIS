namespace DESAFIO_MOVISIS.ViewModels;

public partial class CadastrarLembreteViewModel : ObservableObject
{
    private LembreteValida regasValidacao;
    private IDataStore dataStore;
    private LoadingComponent loadingComponent = new();
    private string base64 {  get; set; }

    [ObservableProperty]
    private bool isErro = false;

    [ObservableProperty]
    private string nomeDoArquivo = string.Empty;

    [ObservableProperty]
    private Lembrete lembrete = new();

    public CadastrarLembreteViewModel(IDataStore dataStore)
    {
        this.dataStore = dataStore;
        Lembrete.Vencimento = DateTime.Now;
        Lembrete.Hora = TimeSpan.FromHours(DateTime.Now.Hour);
        regasValidacao = new LembreteValida();
    }

    [RelayCommand]
    private async Task SelecionarArquivo()
    {
        FileResult? resultado = await SelecionarArquivoService.AbrirSelecionar();
        NomeDoArquivo = resultado?.FileName ?? string.Empty;
        base64 = await resultado.ConverterBase64();        
    }

    private async Task EnviarImagem()
    {
        if (string.IsNullOrEmpty(base64))
            return;

        ArmazenarImagemResponse? resultadoEnvio = await base64.EnviarImagem();
        Lembrete.Anexo = resultadoEnvio?.data?.url;
    }

    [RelayCommand]
    private async Task SalvarLembrete()
    {

        if (!regasValidacao.Validate(Lembrete).IsValid)
        {
            AvisoSnack(StringsUtil.SalvoErro,Colors.LightCoral, StringsUtil.Entendi);
            IsErro = true;
            return;
        }
        IsErro = false;
        await EnviarImagem();
        await SinalDeProcessamento(await dataStore.CreateUniqueAsync<Lembrete>(Lembrete));
    }

    private void AvisoSnack(string mensagem, Color? color, string acaoText = "")
    {
        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = color ?? Colors.White,
            TextColor = Colors.Black,
            CornerRadius = new CornerRadius(10),
        };
        ISnackbar snackbar = Snackbar.Make(mensagem, null, acaoText, TimeSpan.FromSeconds(5), snackbarOptions);
        snackbar.Show();
    }

    private async Task SinalDeProcessamento(int resultado)
    {        
        var currentPage = Application.Current?.MainPage as Page;
        loadingComponent = new LoadingComponent();
        currentPage?.ShowPopup(loadingComponent);
        await Task.Delay(TimeSpan.FromSeconds(3));
        loadingComponent.Close();
        if (resultado == 1)
        {
            AvisoSnack(StringsUtil.SalvoSucesso, Colors.White, StringsUtil.Entendi);
        }
        else
        {
            AvisoSnack(StringsUtil.SalvoErro, Colors.LightCoral, StringsUtil.Entendi);
        }

    }
}
