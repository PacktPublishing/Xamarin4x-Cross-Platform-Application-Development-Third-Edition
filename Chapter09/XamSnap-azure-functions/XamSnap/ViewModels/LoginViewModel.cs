using System;
using System.Threading.Tasks;

namespace XamSnap
{
    public class LoginViewModel : BaseViewModel
    {
        readonly INotificationService notificationService = ServiceContainer.Resolve<INotificationService>();

        public string UserName { get; set; }

        public string Password { get; set; }

        public async Task Login()
        {
            if (string.IsNullOrEmpty(UserName))
                throw new Exception("Username is blank.");

            if (string.IsNullOrEmpty(Password))
                throw new Exception("Password is blank.");

            IsBusy = true;
            try
            {
                settings.User = await service.Login(UserName, Password);
                settings.Save();

                notificationService.Start(UserName);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

