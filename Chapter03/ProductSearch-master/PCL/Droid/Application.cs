using System;
using Android.App;
using Android.Runtime;

namespace ProductSearch.Droid
{
    [Application]
    public class Application : Android.App.Application
    {
        //This constructor is required
        public Application(IntPtr javaReference, JniHandleOwnership
          transfer) : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            //IoC Registration here

            //Android version of ISettings
            ServiceContainer.Register<ISettings>(() => new DroidSettings(this));
            ServiceContainer.Register<SettingsViewModel>(() => new SettingsViewModel());
        }
    }
}

