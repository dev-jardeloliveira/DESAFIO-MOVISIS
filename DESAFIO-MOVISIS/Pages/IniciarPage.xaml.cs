namespace DESAFIO_MOVISIS.Pages;

public partial class IniciarPage : ContentPage
{
    IniciarViewModel viewModel;

    public IniciarPage(IniciarViewModel viewModel)
	{
		InitializeComponent();
        this.viewModel = viewModel;
        this.BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        viewModel.CarregarDadosCommand.Execute(this);
        calendario.Loaded += Calendario_Loaded;
    }

    private async void Calendario_Loaded(object? sender, EventArgs e)
    {
        calendario.Events = viewModel.eventos;
    }
}