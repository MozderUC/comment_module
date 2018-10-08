using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace comment_system_01.Models
{
    public class Upvote
    {
        public int UpvoteID { get; set; }
     
        public int CommentID { get; set; }
        public int UserID { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
        public DateTime Created { get; set; }        
    }
}