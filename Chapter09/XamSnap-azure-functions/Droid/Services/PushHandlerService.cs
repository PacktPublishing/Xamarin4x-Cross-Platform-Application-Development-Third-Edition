using System;
using Android.App;
using Android.Content;
using Android.Support.V7.App;
using Gcm.Client;

namespace XamSnap.Droid
{
    [Service]
    public class PushHandlerService : GcmServiceBase
    {
        public PushHandlerService() : base(Constants.ProjectId) { }

        protected override void OnRegistered(Context context, string registrationId)
        {
            var notificationService = ServiceContainer.Resolve<INotificationService>();
            notificationService.SetToken(registrationId);
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            string message = intent.Extras.GetString("message");
            if (!string.IsNullOrEmpty(message))
            {
                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

                var notification = new NotificationCompat.Builder(this)
                    .SetContentIntent(PendingIntent.GetActivity(this, 0, new Intent(this, typeof(LoginActivity)), 0))
                    .SetSmallIcon(Android.Resource.Drawable.SymActionEmail)
                    .SetAutoCancel(true)
                    .SetContentTitle("XamSnap")
                    .SetContentText(message)
                    .Build();
                notificationManager.Notify(1, notification);
            }
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Console.WriteLine("GCM unregistered!");
        }

        protected override void OnError(Context context, string errorId)
        {
            Console.WriteLine("GCM Error: " + errorId);
        }
    }
}
