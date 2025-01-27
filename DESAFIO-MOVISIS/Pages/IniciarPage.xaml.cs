namespace DESAFIO_MOVISIS.Pages;

public partial class IniciarPage : ContentPage
{
	public IniciarPage(IniciarViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}