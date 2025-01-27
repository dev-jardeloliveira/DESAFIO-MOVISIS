

namespace DESAFIO_MOVISIS.Pages;

public partial class AutenticarPage : ContentPage
{
	public AutenticarPage(AutenticarViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}