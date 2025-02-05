namespace DESAFIO_MOVISIS.ViewModels;

public partial class IniciarViewModel : ObservableObject
{
    private readonly LembreteCasoUso casoUso;

    private IDataStore dataStore;

    public EventCollection eventos { get; set; } = new();

    private Usuario usuario { get; set; }

    [ObservableProperty]
    private CultureInfo cultura = CultureUtil.cultureInfo;

    [ObservableProperty]
    private DateTime dataHoraAtual = DateTime.Now;

    [ObservableProperty]
    private WeekLayout layoutWeek;

    public IniciarViewModel(LembreteCasoUso casoUso, IDataStore dataStore)
    {
        this.casoUso = casoUso;
        this.dataStore = dataStore;
        Task.Factory.StartNew(async() =>  await this.dataStore.SingleAsync<DataStores.Models.Usuario>()).ContinueWith(async it=> {
            var resultado = await it.Result;
            if (it.IsCompletedSuccessfully)
            {
                usuario = resultado;
                await CarregarDados();
            }        
        });

    }

    [RelayCommand]
    private async Task CarregarDados()
    {       
        List<Dados_App.Modelo.LembreteResponse> lembretes = await casoUso.Todos(usuario.Token!, Guid.Parse(usuario.Guid!));

        if (lembretes?.Count > 0)
        {
                eventos = new EventCollection();
                lembretes.GroupBy(it => it.vencimento.Date).ToList().ForEach(it =>
                {
                    eventos.Add(it.Key, it.ToList());
                });          

            lembretes.ForEach(it => LembretesNotificacao.ListaDeLemebretes.Add(LembreteMapper.MapearResponseParaDto(it)));

        }
    }
    
    [RelayCommand]
    public void AlternarLayout()
    {
        switch (LayoutWeek)
        {
            case WeekLayout.Week:
                LayoutWeek = WeekLayout.Month;
                break;
            case WeekLayout.Month:
                LayoutWeek = WeekLayout.Week;
                break;
            default:
                LayoutWeek = WeekLayout.Week;
                break;
        }
    }
       
    [RelayCommand]
    public void AdicionarNovoEvento()
    {
        var navigationParameter = new ShellNavigationQueryParameters
        {
            { "Lembrete", new Lembrete{  Vencimento = DateTime.Now, Hora = TimeSpan.FromHours(DateTime.Now.Hour)} }
        };
        Shell.Current.GoToAsync("cadastrar_lembrete",navigationParameter);
    }

    [RelayCommand]
    public async Task RemoverEvento(Dados_App.Modelo.LembreteResponse lembrete)
    {
        bool resultado = await Application.Current.MainPage.DisplayAlert("Excluir", "Deseja confirmar a exclusão deste item?", "Sim", "Não");

        if (resultado)
        {
            await casoUso.Excluir(Guid.Parse(lembrete.id!), usuario.Token!).ContinueWith(async it =>
            {
                if (it.IsCompletedSuccessfully)
                {
                    var resultado = it.Result;
                    if(resultado == "1")
                    {
                        AvisoSnack(StringsUtil.ExcluirSucesso,Colors.White);
                        await CarregarDados();
                    }
                    else
                    {
                        AvisoSnack(StringsUtil.ExcluirErro, Colors.LightCoral);
                    }
                }
            } );
        }
    }

    [RelayCommand]
    public async Task EditarEvento(Dados_App.Modelo.LembreteResponse lembrete)
    {
        Lembrete lembreteModelo = LembreteMapper.MapearResponseParaModelo(lembrete);
        var navigationParameter = new ShellNavigationQueryParameters
        {
            { "Lembrete", lembreteModelo }
        };
        await Shell.Current.GoToAsync("cadastrar_lembrete", navigationParameter);
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

   
}
