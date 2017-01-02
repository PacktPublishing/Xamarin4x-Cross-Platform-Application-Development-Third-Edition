using System;

namespace XamSnap
{
    public interface ISettings
    {
        User User { get; set; }

        void Save();
    }
}

