using System;
using System.Net.Http;
using System.Threading.Tasks;
using NUnit.Framework;

namespace XamSnap.Tests
{
    [TestFixture]
    public class AzureFunctionsTests
    {
        private AzureWebService _service;

        [SetUp]
        public void SetUp()
        {
            _service = new AzureWebService();
        }

        [Test]
        public async Task LoginNewUser()
        {
            var user = await _service.Login(Guid.NewGuid().ToString("N"), "Woot");

            Assert.IsNotNull(user);
        }

        [Test]
        public async Task LoginExisting()
        {
            var user = await _service.Login("Azure", "Woot");

            Assert.IsNotNull(user);
        }

        [Test, ExpectedException(typeof(HttpRequestException))]
        public async Task LoginExistingUserWrongPassword()
        {
            var user = await _service.Login("Azure", "aslidfjsalkf");

            Assert.IsNotNull(user);
        }

        [Test]
        public async Task GetFriends()
        {
            var friends = await _service.GetFriends("luke");

            Assert.AreNotEqual(0, friends);
        }

        [Test]
        public async Task AddFriend()
        {
            string id = Guid.NewGuid().ToString("N");
            var user = await _service.AddFriend(Guid.NewGuid().ToString("N"), id);
            Assert.AreEqual(id, user.Name);
        }

        [Test]
        public async Task GetConversations()
        {
            var conversations = await _service.GetConversations("luke");

            Assert.AreNotEqual(0, conversations);
        }

        [Test]
        public async Task GetMessages()
        {
            var messages = await _service.GetMessages("1");

            Assert.AreNotEqual(0, messages);
        }

        [Test]
        public async Task SendMessage()
        {
            var message = await _service.SendMessage(new Message
            {
                Conversation = "1",
                Id = "5",
                UserName = "luke",
                Text = "asfadsfanlnk",
            });

            Assert.AreEqual("1", message.Conversation);
        }
    }
}
