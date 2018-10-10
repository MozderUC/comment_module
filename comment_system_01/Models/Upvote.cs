using comment_system_01.Models.Account;
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
        public string UserID { get; set; }

        public virtual Comment Comment { get; set; }
        public virtual ApplicationUser User { get; set; }
        public DateTime Created { get; set; }        
    }
}