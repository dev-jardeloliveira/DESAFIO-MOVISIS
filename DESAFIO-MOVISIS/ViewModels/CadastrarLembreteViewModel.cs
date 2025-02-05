namespace DESAFIO_MOVISIS.ViewModels;

public partial class CadastrarLembreteViewModel : ObservableObject, IQueryAttributable
{   
    private readonly LembreteCasoUso casoUso;
    private LembreteValida regasValidacao;
    private IDataStore dataStore;
    private LoadingComponent loadingComponent = new();
    private string base64 {  get; set; }
    private bool isAlterar = false;
    [ObservableProperty]
    private bool isErro = false;

    [ObservableProperty]
    private bool isAnexo = false;

    [ObservableProperty]
    private string nomeDoArquivo = string.Empty;

    [ObservableProperty]
    private Lembrete lembrete = new();

    public CadastrarLembreteViewModel(IDataStore dataStore, LembreteCasoUso casoUso)
    {
        regasValidacao = new LembreteValida();
        this.dataStore = dataStore;
        this.casoUso = casoUso;
        Lembrete.Vencimento = DateTime.Now;
        Lembrete.Hora = TimeSpan.FromHours(DateTime.Now.Hour);

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

        var usuario = await this.dataStore.SingleAsync<Usuario>();
        Lembrete.IdUsuario = usuario.Guid;
        var mapperLembrete = LembreteMapper.MapearParaLembreteDados(Lembrete);
        if (isAlterar)
        {
          await SinalDeProcessamento(await casoUso.Alterar(mapperLembrete, usuario.Token!));
        }
        else
        {
          await SinalDeProcessamento(await casoUso.Gravar(mapperLembrete, usuario.Token!));

        }

    }

    [RelayCommand]
    private async Task AbrirAnexo()
    {
        if (Uri.TryCreate(Lembrete.Anexo, UriKind.Absolute, out var uri))
        {
            await Launcher.OpenAsync(uri);
        }
        else
        {
            Console.WriteLine("URL inválida.");
        }
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

    private async Task SinalDeProcessamento(string resultado)
    {        
        var currentPage = Application.Current?.MainPage as Page;
        loadingComponent = new LoadingComponent();
        currentPage?.ShowPopup(loadingComponent);
        await Task.Delay(TimeSpan.FromSeconds(3));
        loadingComponent.Close();
        if (resultado == "Sucesso")
        {
            AvisoSnack(StringsUtil.SalvoSucesso, Colors.White, StringsUtil.Entendi);
            await Shell.Current.GoToAsync("iniciar");
        }
        else
        {
            AvisoSnack(StringsUtil.SalvoErro, Colors.LightCoral, StringsUtil.Entendi);
        }

    }


    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Lembrete = (Lembrete)query["Lembrete"];
        OnPropertyChanged("Lembrete");

        isAlterar = !string.IsNullOrEmpty(Lembrete.Titulo);

        if (!string.IsNullOrEmpty(Lembrete.Anexo))
        {
            if (Uri.TryCreate(Lembrete.Anexo, UriKind.Absolute, out var uri))
                IsAnexo = true;
        }
    }
}
