using System;
using System.Threading.Tasks;

namespace XamSnap
{
    public class FakeWebService : IWebService
    {
        public int SleepDuration { get; set; }

        public FakeWebService()
        {
            SleepDuration = 1000;
        }

        private Task Sleep()
        {
            return Task.Delay(SleepDuration);
        }

        public async Task<User> Login(string userName, string password)
        {
            await Sleep();

            return new User { Name = userName };
        }

        public async Task<User> Register(User user)
        {
            await Sleep();

            return user;
        }

        public async Task<User[]> GetFriends(string userName)
        {
            await Sleep();

            return new[]
            {
                new User { Name = "bobama" },
                new User { Name = "bobloblaw" },
                new User { Name = "georgemichael" },
            };
        }

        public async Task<User> AddFriend(string userName, string friendName)
        {
            await Sleep();

            return new User { Name = friendName };
        }

        public async Task<Conversation[]> GetConversations(string userName)
        {
            await Sleep();

            return new[]
            {
                new Conversation { Id = "1", UserName = "bobama" },
                new Conversation { Id = "2", UserName = "bobloblaw" },
                new Conversation { Id = "3", UserName = "georgemichael" },
            };
        }

        public async Task<Message[]> GetMessages(string conversation)
        {
            await Sleep();

            return new[]
            {
                new Message
                {
                    Id = "1",
                    Conversation = conversation,
                    UserName = "bobloblaw",
                    Text = "Hey",
                },
                new Message
                {
                    Id = "2",
                    Conversation = conversation,
                    UserName = "georgemichael",
                    Text = "What's Up?",
                },
                new Message
                {
                    Id = "3",
                    Conversation = conversation,
                    UserName = "bobloblaw",
                    Text = "Have you seen that new movie?",
                },
                new Message
                {
                    Id = "4",
                    Conversation = conversation,
                    UserName = "georgemichael",
                    Text = "It's great!",
                },
            };
        }

        public async Task<Message> SendMessage(Message message)
        {
            await Sleep();

            return message;
        }
    }
}

