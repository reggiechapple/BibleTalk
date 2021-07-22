using System.Collections.Generic;
using BibleTalk.Data.Identity;

namespace BibleTalk.Data.Entities
{
    public class Post : Entity
    {
        public string Headline { get; set; }
        public string Image { get; set; }
        public string Body { get; set; }

        public long AuthorId { get; set; }
        public Member Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}