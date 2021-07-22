using BibleTalk.Data.Identity;

namespace BibleTalk.Data.Entities
{
    public class Follow
    {
        public long FollowerId { get; set; }
        public Member Follower { get; set; }

        public long FollowedId { get; set; }
        public Member Followed { get; set; }
    }
}