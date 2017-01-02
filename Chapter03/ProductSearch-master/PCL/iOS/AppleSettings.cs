using System;
using UIKit;
using Foundation;

namespace ProductSearch.iOS
{
    public class AppleSettings : ISettings
    {
        public bool IsSoundOn
        {
            get { return NSUserDefaults.StandardUserDefaults.BoolForKey("IsSoundOn"); }
            set
            {
                var defaults = NSUserDefaults.StandardUserDefaults;
                defaults.SetBool(value, "IsSoundOn");
                defaults.Synchronize();
            }
        }
    }
}

