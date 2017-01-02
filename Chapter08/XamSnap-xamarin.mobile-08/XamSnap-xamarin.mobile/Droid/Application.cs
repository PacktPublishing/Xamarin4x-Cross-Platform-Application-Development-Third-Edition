using System;
using Android.App;
using Android.Runtime;

namespace XamSnap.Droid
{
    [Application(Theme = "@style/Theme.AppCompat.Light")]
    public class Application : Android.App.Application
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();

            //ViewModels
            ServiceContainer.Register<LoginViewModel>(() => new LoginViewModel());
            ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
            ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());
            ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());

            //Models
            ServiceContainer.Register<ISettings>(() => new FakeSettings());
            ServiceContainer.Register<IWebService>(() => new FakeWebService());
            ServiceContainer.Register<IFriendService>(() => new ContactsService());
            ServiceContainer.Register<ILocationService>(() => new LocationService());
        }
    }
}

