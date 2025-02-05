using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotification;

namespace DESAFIO_MOVISIS
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                IniciarNotificacoes();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }

        private void IniciarNotificacoes()
        {
            Timer _timer = new Timer(EnviarNotificacao!, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async void EnviarNotificacao(object state)
        {
            var agora = DateTime.Now;
            var dataAtual = agora.Date;
            var horaAtual = new TimeSpan(agora.Hour, agora.Minute,0);

            var lembretes = LembretesNotificacao.ListaDeLemebretes.Where(it =>
                it.Vencimento.HasValue
                && it.Vencimento.Value.Date == dataAtual
                && it.Hora.HasValue
                && it.Hora.Value.Subtract(TimeSpan.FromMinutes(30)) == horaAtual);

            if (lembretes.Count() > 0)
                foreach (var it in lembretes)
                {
                    var notification = new NotificationRequest
                    {
                        NotificationId = 100,
                        Title = it.Titulo!,
                        Description = it.Descricao!,
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = DateTime.Now
                        }
                    };

                    await LocalNotificationCenter.Current.Show(notification);
                }

        }

    }
}
