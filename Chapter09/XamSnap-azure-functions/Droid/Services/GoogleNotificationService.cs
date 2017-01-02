using Android.Content;
using System;
using Gcm.Client;
using WindowsAzure.Messaging;

namespace XamSnap.Droid
{
    public class GoogleNotificationService : INotificationService
    {
        readonly Context context;
        NotificationHub hub;
        string userName;

        public GoogleNotificationService(Context context)
        {
            this.context = context;
        }

        public void SetToken(object deviceToken)
        {
            hub = new NotificationHub(Constants.HubName, Constants.ConnectionString, context);
            try
            {
                string template = "{\"data\":{\"message\":\"$(message)\"}}";
                hub.RegisterTemplate((string)deviceToken, "Android", template, userName);
            }
            catch (Exception exc)
            {
                Console.WriteLine("RegisterTemplate Error: " + exc.Message);
            }
        }

        public void Start(string userName)
        {
            this.userName = userName;
            GcmClient.CheckDevice(context);
            GcmClient.CheckManifest(context);
            GcmClient.Register(context, Constants.ProjectId);
        }
    }
}
