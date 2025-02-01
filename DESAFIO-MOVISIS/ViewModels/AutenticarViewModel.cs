namespace DESAFIO_MOVISIS.ViewModels;

public partial class AutenticarViewModel : ObservableObject
{
    public ICommand? AnimacaoCommand { get; set; }
    private IDataStore dataStore;
    private LoadingComponent loadingComponent = new();
    private readonly UsuarioCasoUso casoUso;

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


    public AutenticarViewModel(IDataStore dataStore, UsuarioCasoUso casoUso)
    {
        this.casoUso = casoUso;
        this.dataStore = dataStore;

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
        await Task.Delay(TimeSpan.FromSeconds(5));

        if (!AutenticarUsuario.Email.IsValidarEmail() || !AutenticarUsuario.Senha.IsValidarString(6))
        {
            AvisoSnack(StringsUtil.ErroCampos, Colors.LightCoral);
            return;
        }

        UsuarioDto dto = UsuarioMapper.ToDto(AutenticarUsuario);

        var usuario = UsuarioMapper.ToUsuario(dto);
        var dadosDeEntrada = UsuarioMapper.ToDados(usuario);

        await VerificarApi(usuario, dadosDeEntrada);

    }

    private async Task VerificarApi(Usuario usuario, Dados_App.Modelo.Usuario dadosDeEntrada)
    {
        await casoUso.Verificar(dadosDeEntrada).ContinueWith(async it =>
        {

            if (it.IsCompletedSuccessfully)
            {
                usuario.Token = it?.Result?.token;
                usuario.Guid = it?.Result?.id;
                await this.dataStore.CreateUniqueAsync<Usuario>(usuario);

                await Shell.Current.GoToAsync("//iniciar");
            }
            else
            {
                AvisoSnack(StringsUtil.ErroCampos, Colors.LightCoral);
            }
        });
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

        var NovoUsuario = new Dados_App.Modelo.Usuario { Id = Guid.NewGuid(), Email = AutenticarUsuario!.Email, Senha = AutenticarUsuario!.Senha.Criptografar() };

        await casoUso.Gravar(NovoUsuario).ContinueWith(it => {

            if (it.IsCompletedSuccessfully)
            {
                AvisoSnack(StringsUtil.CadastradoSucesso, Colors.White);
            }
            else
            {
                AvisoSnack(StringsUtil.SalvoErro, Colors.LightCoral);
            }
        });

        
    }

    async Task AutenticarComBiometriaAsync()
    {
        await Task.Delay(TimeSpan.FromSeconds(15));

        var usuario = await this.dataStore.AllAsync<Usuario>();

        if (usuario?.Count() < 1)
            return;

        var usuarioCadastrado = usuario?.FirstOrDefault();

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
            var dadosDeEntrada = UsuarioMapper.ToDados(usuarioCadastrado!);
            await VerificarApi(usuarioCadastrado!, dadosDeEntrada);
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
