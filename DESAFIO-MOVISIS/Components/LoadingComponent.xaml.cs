using CommunityToolkit.Maui.Views;

namespace DESAFIO_MOVISIS.Components;

public partial class LoadingComponent : Popup
{
    public LoadingComponent()
    {
        InitializeComponent();
        BindingContext = this;
    }

}