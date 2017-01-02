using Foundation;
using UIKit;

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
            ServiceContainer.Register<IWebService>(() => new FakeWebService());

            return true;
        }
    }
}


