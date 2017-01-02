using Foundation;
using UIKit;
using System;

namespace XamSnap.iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            //View Models
            ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
            ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
            ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());
            ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());

            //Models
            ServiceContainer.Register<ISettings>(() => new FakeSettings());
            ServiceContainer.Register<IWebService>(() => new AzureWebService());
            ServiceContainer.Register<IFriendService>(() => new AzureWebService());
            ServiceContainer.Register<ILocationService>(() => new LocationService());
            ServiceContainer.Register<INotificationService>(() => new AppleNotificationService());

            return true;
        }

        public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
        {
            application.RegisterForRemoteNotifications();
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            var notificationService = ServiceContainer.Resolve<INotificationService>();
            notificationService.SetToken(deviceToken);
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            Console.WriteLine("Failed to Register: " + error.LocalizedDescription);
        }

        public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
        {
            
        }
    }
}


