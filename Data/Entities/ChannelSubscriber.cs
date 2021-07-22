using BibleTalk.Data.Identity;

namespace BibleTalk.Data.Entities
{
    public class BibleTalkubscriber
    {
        public long ChannelId { get; set; }
        public Channel Channel { get; set; }

        public long SubscriberId { get; set; }
        public Member Subscriber { get; set; }
    }
}