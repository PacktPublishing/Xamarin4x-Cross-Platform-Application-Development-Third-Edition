using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamSnap
{
    public class AzureWebService : IWebService, IFriendService
    {
        private const string BaseUrl = "https://xamsnap.azurewebsites.net/api/";
        private const string ContentType = "application/json";
        private readonly HttpClient httpClient = new HttpClient();

        private async Task<HttpResponseMessage> Post(string url, string code, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue(ContentType);

            var response = await httpClient.PostAsync(BaseUrl + url + "?code=" + code, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async Task<T> Post<T>(string url, string code, object obj)
        {
            var response = await Post(url, code, obj);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<User> AddFriend(string userName, string friendName)
        {
            await Post("addfriend", "cyp0asakll1yfdus4vsk5u3dieves1y4rlldj0lphoj013v7viw9zxsb4vzxn3cy6rafmg9cnmi", new
            {
                userName,
                friendName
            });

            return new User
            {
                Name = friendName
            };
        }

        public Task<User[]> GetFriends(string userName)
        {
            return Post<User[]>("friends", "w0to2o614csk8e3iduc8fri7erkowfmuoavgxb6g2o11yvin6achwnsgecxgg6wqyeigrpb9", new
            {
                userName
            });
        }

        public Task<Conversation[]> GetConversations(string userName)
        {
            return Post<Conversation[]>("conversations", "hsfqtj34ejgmujbpnxyjy8pvi79lgvj19bai5u9htd477tu766re5fpy1vm77vtn2imeyrkbuik9", new
            {
                userName
            });
        }

        public Task<Message[]> GetMessages(string conversation)
        {
            return Post<Message[]>("messages", "af6rdk8tdnx0hqi0gph7zxgvihmiu5k1l86j1ihrgtbs0v0a4ifai28nifb3q3zz3lwr3cba9k9", new
            {
                conversation
            });
        }

        public async Task<User> Login(string userName, string password)
        {
            await Post("login", "5n2x69ueeoszzcz49900ctbj4i8yohucx7gbaad7taideoecdiw0gktlmu847b4vwss8dw2vs4i", new
            {
                userName,
                password,
            });

            return new User
            {
                Name = userName,
                Password = password,
            };
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Message> SendMessage(Message message)
        {
            message.Id = Guid.NewGuid().ToString("N");

            await Post("sendmessage", "v7z2tg7pprxb1f3vazmjwcdikuq9ql55wft1glcft1rsmunmi52vlomrm2ysuoaeg3d4vgta9k9", message);

            return message;
        }
    }
}
