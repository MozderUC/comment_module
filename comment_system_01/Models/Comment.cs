using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace comment_system_01.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int MovieID { get; set; }
        public int UserID { get; set; }
        public string Context { get; set; }
        public int? Parent { get; set; }
        public int Upvote_count { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }


        public virtual Movie Movie { get; set; }
        public virtual User User { get; set; }

    }
}