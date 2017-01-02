using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XamSnap.Tests
{
    [TestFixture]
    public class LoginViewModelTests : BaseTest
    {
        LoginViewModel loginViewModel;
        ISettings settings;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            settings = ServiceContainer.Resolve<ISettings>();
            loginViewModel = new LoginViewModel();
        }

        [Test]
        public async Task LoginSuccessfully()
        {
            loginViewModel.UserName = "testuser";
            loginViewModel.Password = "password";

            await loginViewModel.Login();

            Assert.That(settings.User, Is.Not.Null);
        }

        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "Username is blank.")]
        public async Task LoginWithNoUsernameOrPassword()
        {
            //Throws an exception
            await loginViewModel.Login();
        }
    }
}

