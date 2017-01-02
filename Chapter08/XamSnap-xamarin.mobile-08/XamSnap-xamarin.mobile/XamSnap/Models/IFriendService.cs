using System.Threading.Tasks;

namespace XamSnap
{
    public interface IFriendService
    {
        Task<User[]> GetFriends(string userName);
    }
}

