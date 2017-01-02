using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace XamSnap.Droid
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class LoginActivity : BaseActivity<LoginViewModel>
    {
        EditText username, password;
        Button login;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.login);
            login.Click += OnLogin;
        }

        protected override void OnResume()
        {
            base.OnResume();
            username.Text =
              password.Text = string.Empty;
        }

        async void OnLogin(object sender, EventArgs e)
        {
            viewModel.UserName = username.Text;
            viewModel.Password = password.Text;
            try
            {
                await viewModel.Login();

                StartActivity(typeof(ConversationsActivity));
            }
            catch (Exception exc)
            {
                DisplayError(exc);
            }
        }
    }
}

