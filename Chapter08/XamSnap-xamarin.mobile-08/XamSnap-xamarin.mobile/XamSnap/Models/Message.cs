using System;

namespace XamSnap
{
    public class Message
    {
        public string Id { get; set; }

        //NOTE: the Id of a Conversation
        public string Conversation { get; set; }

        public string UserName { get; set; }

        public string Text { get; set; }

        //NOTE: GPS location of the sender
        public Location Location { get; set; }

        //NOTE: some messages will include photos
        public string Image { get; set; }
    }
}

