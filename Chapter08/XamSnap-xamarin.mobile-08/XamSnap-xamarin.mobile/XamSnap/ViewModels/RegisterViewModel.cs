using System;
using System.Threading.Tasks;

namespace XamSnap
{
    public class RegisterViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public async Task Register()
        {
            if (string.IsNullOrEmpty(UserName))
                throw new Exception("Username is blank.");

            if (string.IsNullOrEmpty(Password))
                throw new Exception("Password is blank.");

            if (Password != ConfirmPassword)
                throw new Exception("Passwords do not match.");

            IsBusy = true;
            try
            {
                settings.User = await service.Register(new User
                {
                    Name = UserName,
                    Password = Password,
                });
                settings.Save();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

