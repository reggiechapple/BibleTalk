using System.Collections.Generic;
using BibleTalk.Data.Entities;

namespace BibleTalk.Data.Identity
{
    public class Member : Profile
    {
        public ICollection<Channel> BibleTalk { get; set; }
        public ICollection<BibleTalkubscriber> Subscriptions { get; set; }
        public ICollection<ChannelMessage> ChannelMessages { get; set; }
        public ICollection<UserNotification> Notifications { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Follow> Follows { get; set; }
        public ICollection<Follow> Following { get; set; }
    }
}