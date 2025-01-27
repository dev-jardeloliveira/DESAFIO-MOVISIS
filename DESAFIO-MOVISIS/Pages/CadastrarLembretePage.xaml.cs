namespace DESAFIO_MOVISIS.Pages;

public partial class CadastrarLembretePage : ContentPage
{
	public CadastrarLembretePage(CadastrarLembreteViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}