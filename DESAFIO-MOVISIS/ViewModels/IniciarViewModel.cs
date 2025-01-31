namespace DESAFIO_MOVISIS.ViewModels;

public partial class IniciarViewModel : ObservableObject
{
    
    public EventCollection eventos { get; set; }

    [ObservableProperty]
    private CultureInfo cultura = CultureUtil.cultureInfo;

    [ObservableProperty]
    private DateTime dataHoraAtual = DateTime.Now;

    [ObservableProperty]
    private WeekLayout layoutWeek;

    public IniciarViewModel()
    {        
        eventos = new EventCollection
        {
            [DateTime.Now] = new List<Lembrete>
            {
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(3),
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(1),
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(2),
                     Anexo = ""
                },
                 new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(3),
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(1),
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(2),
                     Anexo = ""
                },
                 new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(3),
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(1),
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now.AddHours(2),
                     Anexo = ""
                },
            },
           
           
            [DateTime.Now.AddDays(1)] = new List<Lembrete>
            {
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now,
                     Anexo = ""
                },
                new Lembrete{
                    Id = Guid.NewGuid(),
                    Titulo = "Titulo",
                     Descricao = "Descrição",
                     Vencimento = DateTime.Now,
                     Anexo = ""
                },
              
            },
         

        };
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
        Shell.Current.GoToAsync("cadastrar_lembrete");
    }

    [RelayCommand]
    public void RemoverEvento()
    {
    }
}
