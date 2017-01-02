using System;

namespace XamSnap
{
    public class FakeSettings : ISettings
    {
        public User User { get; set; }

        public void Save() { }
    }
}

