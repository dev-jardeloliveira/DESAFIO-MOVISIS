namespace DESAFIO_MOVISIS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Rotas();
        }

        void Rotas()
        {
            Routing.RegisterRoute("cadastrar_lembrete",typeof(CadastrarLembretePage));
        }
    }
}
