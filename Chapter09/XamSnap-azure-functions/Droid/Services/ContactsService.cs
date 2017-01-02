using System.Collections.Generic;
using System.Threading.Tasks;

namespace XamSnap.Droid
{
    public class ContactsService : IFriendService
    {
        public async Task<User[]> GetFriends(string userName)
        {
            var book = new Xamarin.Contacts.AddressBook(Application.Context);
            await book.RequestPermission();

            var users = new List<User>();
            foreach (var contact in book)
            {
                users.Add(new User
                {
                    Name = contact.DisplayName,
                });
            }
            return users.ToArray();
        }
    }
}

