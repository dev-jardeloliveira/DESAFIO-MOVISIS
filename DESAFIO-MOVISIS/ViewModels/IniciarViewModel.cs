namespace DESAFIO_MOVISIS.ViewModels;

public partial class IniciarViewModel : ObservableObject
{
    [ObservableProperty]
    private CultureInfo cultura = CultureUtil.cultureInfo;

    [ObservableProperty]
    private DateTime dataHoraAtual = DateTime.Now;

    public ICommand PreviousMonthCommand { get; }
    public IniciarViewModel()
    {
        PreviousMonthCommand = new Command(() =>
        {
            dataHoraAtual = dataHoraAtual.AddDays(-7);
        });
    }
    [RelayCommand]
    private void SemanaAnterior()
    {
        dataHoraAtual = dataHoraAtual.AddDays(-7);
    }

    [RelayCommand]
    private void ProximaSemana()
    {
        dataHoraAtual = dataHoraAtual.AddDays(7);
    }
}
