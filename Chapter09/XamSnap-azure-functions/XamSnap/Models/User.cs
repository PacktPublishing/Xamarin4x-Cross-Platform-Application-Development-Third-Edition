using System;

namespace XamSnap
{
    public class User
    {
        //NOTE: we will treat this as a unique name
        public string Name { get; set; }

        //NOTE: we’ll try to use this in a secure way
        public string Password { get; set; }
    }
}

