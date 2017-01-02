using System;
using Android.App;

[assembly: Permission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "@PACKAGE_NAME@.permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]

namespace XamSnap.Droid
{
    public static class Constants
    {
        public const string ProjectId = "949356865271";
        public const string ConnectionString = "Endpoint=sb://xamsnap.servicebus.windows.net/;SharedAccessKeyName=DefaultListenSharedAccessSignature;SharedAccessKey=ZqbSdT8i+2YBpNNxmgMNAOrnoGPCBcr+/Hs9gecyNTQ=";
        public const string HubName = "xamsnap";
    }
}
