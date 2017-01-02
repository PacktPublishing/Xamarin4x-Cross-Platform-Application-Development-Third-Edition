using Foundation;
using UIKit;

namespace ProductSearch.iOS
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
            //iOS version of ISettings
            ServiceContainer.Register<ISettings>(() => new AppleSettings());
            ServiceContainer.Register<SettingsViewModel>(() => new SettingsViewModel());

            return true;
        }
    }
}


