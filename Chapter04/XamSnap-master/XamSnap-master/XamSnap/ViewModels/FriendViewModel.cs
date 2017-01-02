using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamSnap
{
    public class FriendViewModel : BaseViewModel
    {
        public User[] Friends { get; private set; }

        public string UserName { get; set; }

        public async Task GetFriends()
        {
            if (settings.User == null)
                throw new Exception("Not logged in.");

            IsBusy = true;
            try
            {
                Friends = await service.GetFriends(settings.User.Name);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task AddFriend()
        {
            if (settings.User == null)
                throw new Exception("Not logged in.");

            if (string.IsNullOrEmpty(UserName))
                throw new Exception("Username is blank.");

            IsBusy = true;
            try
            {
                var friend = await service.AddFriend(settings.User.Name, UserName);

                //Update our local list of friends
                var friends = new List<User>();
                if (Friends != null)
                    friends.AddRange(Friends);
                friends.Add(friend);

                Friends = friends.OrderBy(f => f.Name).ToArray();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

