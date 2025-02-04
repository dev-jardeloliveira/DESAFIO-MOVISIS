using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotification;

namespace DESAFIO_MOVISIS.Platforms.Android.Services
{
    [Service(Exported = true, ForegroundServiceType = ForegroundService.TypeDataSync)]
    public class NotificacaoService : Service
    {
        private Timer _timer;
        private const int NOTIFICATION_ID = 1;
        private const string CHANNEL_ID = "NOTIFICACAO_SERVICE_CHANNEL";

        public override void OnCreate()
        {
            base.OnCreate();

            //CriarCanalNotificacao();
            //StartForeground(NOTIFICATION_ID, CriarNotificacao);
            IniciarNotificacoes();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        private void IniciarNotificacoes()
        {
            _timer = new Timer(EnviarNotificacao, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
        }

        private async void EnviarNotificacao(object state)
        {
            var agora = DateTime.Now;
            var dataAtual = agora.Date;
            var horaAtual = TimeSpan.FromHours(agora.Hour);

            var lembretes = LembretesNotificacao.ListaDeLemebretes.Where(it =>
                it.Vencimento.HasValue &&
                it.Vencimento.Value.Date == dataAtual
            );

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

        private void CriarCanalNotificacao()
        {
            try
            {
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    var channel = new NotificationChannel(CHANNEL_ID, "Serviço de Notificação", NotificationImportance.Default);
                    var manager = (NotificationManager)GetSystemService(NotificationService);
                    manager.CreateNotificationChannel(channel);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar notificação: {ex.Message}");
            }

        }

        private Notification CriarNotificacao()
        {
            Notification.Builder notificationBuilder;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                notificationBuilder = new Notification.Builder(this, CHANNEL_ID);
            }
            else
            {
                notificationBuilder = new Notification.Builder(this)
                    .SetPriority((int)NotificationPriority.Max);
            }
          

            return notificationBuilder
                .SetContentTitle("")
                .SetContentText("")
                .SetSmallIcon(Resource.Drawable.movisis)
                .SetOngoing(false).Build();
        }


        public override IBinder OnBind(Intent intent) => null;

        public override void OnDestroy()
        {
            _timer?.Dispose();
            base.OnDestroy();
        }
    }
}
