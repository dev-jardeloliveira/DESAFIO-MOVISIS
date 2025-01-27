namespace DESAFIO_MOVISIS.ViewModels;

public partial class AutenticarViewModel : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMsgErro))]
    private Autenticar autenticarUsuario = new();

    [ObservableProperty]
    private bool isMsgErro;

    [ObservableProperty]
    private string msgErroSenha = "";

    [ObservableProperty]
    private string txtButton = StringsUtil.Entrar;

    [ObservableProperty]
    private string txtCadastrar = StringsUtil.NCadastrar;

    [ObservableProperty]
    private bool isEntryVisible = false;

    public ICommand? AnimacaoCommand { get; set; }

    private IBiometric biometric;
    private IDataStore dataStore;
    private LoadingComponent loadingComponent;

    public AutenticarViewModel(IBiometric biometric, IDataStore dataStore)
    {
        this.biometric = biometric;
        this.dataStore = dataStore;
        loadingComponent = new LoadingComponent();

        AutenticarComBiometriaAsync().ConfigureAwait(false);
    }

    [RelayCommand]
    private void EsqueceuSenha()
    {
        AvisoSnack(StringsUtil.RecuperarSenha, Colors.LightYellow, StringsUtil.Entendi);
    }

    [RelayCommand]
    private async Task EntrarOuCadastrar()
    {
       
        if (!IsEntryVisible)
        {
            await SinalDeProcessamento(VerifivarUsuario);
        }
        else
        {
            await SinalDeProcessamento(CadastrarUsuario);
        }

    }

    private async Task VerifivarUsuario()
    {
        //TODO - IMPLEMENTAR API AQUI
        await Task.Delay(TimeSpan.FromSeconds(5));

        if(!AutenticarUsuario.Email.IsValidarEmail() || !AutenticarUsuario.Senha.IsValidarString(6))
        {
            AvisoSnack(StringsUtil.ErroCampos, Colors.LightCoral);
            return;
        }

        bool isValido = AutenticarUsuario.Email.Equals("jardel@gmail.com") && AutenticarUsuario.Senha.Equals("123456");
        if (!isValido){

            AvisoSnack(StringsUtil.ErroCampos, Colors.LightCoral);
            return;
        }

        UsuarioDto dto = UsuarioMapper.ToDto(AutenticarUsuario);

        var usuario = UsuarioMapper.ToUsuario(dto);

        await this.dataStore.CreateUniqueAsync<Usuario>(usuario);

        await Shell.Current.GoToAsync("//iniciar");
    }

    [RelayCommand]
    void AlternarFraseReSenhaCadastro()
    {
        IsEntryVisible = !IsEntryVisible;

        AnimacaoCommand?.Execute(CancellationToken.None);

        TxtCadastrar = !IsEntryVisible ? StringsUtil.NCadastrar : StringsUtil.SCadastrar;

        TxtButton = !IsEntryVisible ? StringsUtil.Entrar : StringsUtil.Cadastrar;
    }

    void AvisoSnack(string mensagem, Color? color, string acaoText = "")
    {
        var snackbarOptions = new SnackbarOptions
        {
            BackgroundColor = color?? Colors.White,
            TextColor = Colors.Black,
            CornerRadius = new CornerRadius(10),
        };
        ISnackbar snackbar = Snackbar.Make(mensagem, null, acaoText, TimeSpan.FromSeconds(5), snackbarOptions);
        snackbar.Show();
    }

    async Task  CadastrarUsuario()
    {
        //TODO - IMPLEMENTAR API AQUI
        await Task.Delay(TimeSpan.FromSeconds(5));

        if ( !AutenticarUsuario!.Email.IsValidarEmail() || !AutenticarUsuario!.Senha.IsValidarString(6))
        {
            IsMsgErro = true;
            MsgErroSenha = StringsUtil.Senha_Erro;
            AvisoSnack(StringsUtil.ErroCampos, Colors.LightCoral);
            return;
        }

        if (!AutenticarUsuario!.ConfirmarSenha.IsValidarConteudoDosCampos(AutenticarUsuario!.Senha))
        {
            AvisoSnack(StringsUtil.ErroCampoSenha, Colors.LightYellow, StringsUtil.Entendi);
            return;
        }

        var NovoUsuario = new Autenticar(Guid.NewGuid(), AutenticarUsuario!.Email,AutenticarUsuario!.Senha);

        AvisoSnack(StringsUtil.CadastradoSucesso, Colors.White);
    }

    async Task AutenticarComBiometriaAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(15));

        var status = await BiometricAuthenticationService.Default.GetAuthenticationStatusAsync();

        if (!BiometricAuthenticationService.Default.IsPlatformSupported)
            return;

        if (status != BiometricHwStatus.Success)
            return;


        var result = await BiometricAuthenticationService.Default.AuthenticateAsync(new AuthenticationRequest()
        {
            Title = "Autenticação",
            NegativeText = "Cancelar está autenticação"
        }, CancellationToken.None);

        if (result.Status == BiometricResponseStatus.Success)
        {
            await Shell.Current.GoToAsync("//iniciar");
        }
      
    }

    async Task SinalDeProcessamento(Func<Task> func)
    {
        var currentPage = Application.Current?.MainPage as Page;
        loadingComponent = new LoadingComponent();
        currentPage?.ShowPopup(loadingComponent);

        await func.Invoke().ContinueWith(it =>
        {
            if (it.IsCompleted)
            {
                loadingComponent.Close();
            }
        });       
            
    }

}
