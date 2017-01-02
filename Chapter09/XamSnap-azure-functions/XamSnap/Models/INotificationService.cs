using System;

namespace XamSnap
{
    public interface INotificationService
    {
        void Start(string userName);

        void SetToken(object deviceToken);
    }
}
