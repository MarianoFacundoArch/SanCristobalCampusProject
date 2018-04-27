using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using InvestigacionSocial.BackgroundService;
using Xamarin.Forms;

namespace InvestigacionSocial.Droid
{
    [Service]
    public class LongRunningTaskService : Android.App.Service
    {
        CancellationTokenSource _cts;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }


        PendingIntent pendingIntent;
        NotificationManager notificationManager;
        Notification ongoing;


        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(LongRunningTaskService)), 0);
            notificationManager = (NotificationManager)GetSystemService(NotificationService);
            ongoing = new Notification(Resource.Drawable.icon, "Notification");

            ongoing.SetLatestEventInfo(this, "Servicio de gruas", "Servicio corriendo.", pendingIntent);
            ongoing.Flags |= NotificationFlags.ForegroundService;
            //ongoing.Flags |= NotificationFlags.AutoCancel;
            notificationManager.Notify(2, ongoing);


            _cts = new CancellationTokenSource();

            Task.Run(() => {
                try
                {


                    //INVOKE THE SHARED CODE
                    var counter = new LocationService();
                    counter.RunCounter(_cts.Token).Wait();
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    if (_cts.IsCancellationRequested)
                    {
                        var message = new CancelledMessage();
                        Device.BeginInvokeOnMainThread(
                            () => MessagingCenter.Send(message, "CancelledMessage")
                        );
                    }

                    //notificationManager.Cancel(2);
                }

            }, _cts.Token);

            return StartCommandResult.Sticky;
        }

        public override void OnDestroy()
        {
            //notificationManager.Cancel(2);
            if (_cts != null)
            {
                _cts.Token.ThrowIfCancellationRequested();

                _cts.Cancel();
            }
            base.OnDestroy();
        }
    }
}