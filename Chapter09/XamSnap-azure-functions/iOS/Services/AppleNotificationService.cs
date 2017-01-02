using System;
using Foundation;
using UIKit;
using WindowsAzure.Messaging;
using System.Globalization;

namespace XamSnap.iOS
{
    public class AppleNotificationService : INotificationService
    {
        private readonly CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
        private SBNotificationHub hub;
        private string userName;

        public void Start(string userName)
        {
            this.userName = userName;

            var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);

            UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
        }

        public void SetToken(object deviceToken)
        {
            if (hub == null)
            {
                hub = new SBNotificationHub("Endpoint=sb://xamsnap.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=ZqbSdT8i+2YBpNNxmgMNAOrnoGPCBcr+/Hs9gecyNTQ=", "xamsnap");
            }

            string template = "{\"aps\": {\"alert\": \"$(message)\"}}";
            var tags = new NSSet(userName);
            hub.RegisterTemplateAsync((NSData)deviceToken, "iOS", template, DateTime.Now.AddDays(90).ToString(enUS), tags, errorCallback =>
            {
                if (errorCallback != null)
                    Console.WriteLine("RegisterNativeAsync error: " + errorCallback);
            });
        }
    }
}
